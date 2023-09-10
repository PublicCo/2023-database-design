using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using SqlSugar.Extensions;
using test.Common.Db;
using test.Module.Entities;
using test.Module.Enum;
using test.Service.User.Dto;

namespace test.Service.User
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<Object> CheckLogin(LoginDto login)
        {
            switch(login.userType)
            {
                case (int)user_type.USER:
                    {
                        return await DbContext.db.Queryable<user_info>().FirstAsync(m => m.user_id.Equals(login.id) && m.pwd.Equals(login.pwd));
                    }
                case (int)user_type.WORKER:
                    {
                        return await DbContext.db.Queryable<worker_info>().FirstAsync(m => m.worker_id.Equals(login.id) && m.pwd.Equals(login.pwd));
                    }
                case (int)user_type.GOV:
                    {
                        return await DbContext.db.Queryable<gov_info>().FirstAsync(m => m.gov_id.Equals(login.id) && m.pwd.Equals(login.pwd));
                    }
                default:
                    {
                        throw new Exception("无此用户类型");
                    }
            }
            throw new Exception("传入参数错误");
        }
        public user_info AddUser(InputUserDto input)
        {
            user_info user = TransInputDto(input);

            if (!DbContext.db.Queryable<user_info>().Any(m => m.user_id.Equals(input.user_id)))
            {
                DbContext.db.Insertable(user).ExecuteCommand();
                return user;
            }
            else
                throw new Exception("id已存在");
        }
        private user_info TransInputDto(InputUserDto input)
        {
            var user = _mapper.Map<user_info>(input);
            return user;
        }

        /// <summary>
        /// 根据pos_id返回地区信息
        /// </summary>
        /// <param name="pos_id"></param>
        /// <returns></returns>
        public List<regioninfo> PosIdGetLocation(string pos_id)
        {
            List<regioninfo> Regioninfo = DbContext.db.Queryable<regioninfo>()
                        .Where(it => it.pos_id == pos_id).ToList();
            return Regioninfo;
        }
        /// <summary>
        /// 返回用户的健康信息表的数据库查询操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<user_health_state> GetUserHealthInfo(string id)
        {
            List<user_health_state> user_Health_state = DbContext.db.Queryable<user_health_state>()
                        .Where(it => it.user_id == id).ToList();
            return user_Health_state;
        }

        /// <summary>
        /// 修改用户信息的数据库操作
        /// </summary>
        /// <returns></returns>
        public int ModifyUserInfo(string id, string name, string gender, string password, string phone_number, string user_address)
        {
            ///默认一定能查询到用户信息
            List<user_info> Userinfos = DbContext.db.Queryable<user_info>().Where(it => it.user_id == id).ToList();
            if (Userinfos.Count == 0)
            {
                return -1;
            }
            user_info Userinfo = Userinfos[0];
            DbContext.db.Tracking(Userinfo);//创建跟踪
            Userinfo.user_name = name;
            Userinfo.gender = gender;
            Userinfo.pwd = password;
            Userinfo.phone_number = phone_number;
            Userinfo.user_address = user_address;
            int result = DbContext.db.Updateable(Userinfo).ExecuteCommand();
            return result;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetUserInfo(string id)
        {
            List<user_info> Userinfos = DbContext.db.Queryable<user_info>().Where(it => it.user_id == id).ToList();
            if (Userinfos.Count == 0)
            {
                return "";
            }
            user_info Userinfo = Userinfos[0];
            string data;
            data = JsonConvert.SerializeObject(new
            {
                name = Userinfo.user_name,
                gender = Userinfo.gender,
                password = Userinfo.pwd,
                phone_number = Userinfo.phone_number,
                user_address = Userinfo.user_address,
                user_id = Userinfo.user_id,
                code = 200,
                message = "success"
            });
            return data;
        }
    }
}
