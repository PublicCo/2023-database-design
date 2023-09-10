using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using Newtonsoft.Json;
using SqlSugar;
using test.Common.Db;
using test.Module.Entities;


namespace test.Service.Management.Antigen
{
    public class AntigenService : IAntigenService
    {
        private readonly IMapper _mapper;
        public AntigenService(IMapper mapper)
        {
            _mapper = mapper;
        }
        /// <summary>
        /// 用户上传抗原的数据库操作
        /// </summary>
        /// <param name="id"></param>
        /// <param name="test_time"></param>
        /// <param name="test_result"></param>
        /// <returns></returns>
        public int CheckAndUploadantigen(string id, DateTime test_time, string test_result)
        {
            ///数据库写入操作
            ///
            antigen_record antigen_Record = new antigen_record()
            {
                user_id = id,
                test_time = test_time,
                test_result = test_result
            };
            ///返回受影响条数
            return DbContext.db.Insertable(antigen_Record).ExecuteCommand();
        }
        /// <summary>
        /// 获取用户的抗原检测记录的数据库操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<antigen_record_dto> GetUserAntigRecords(string id)
        {
            List<antigen_record_dto> mid = DbContext.db.Queryable<antigen_record>().Where(it => it.user_id == id).Select(x => new antigen_record_dto()
            {
                test_time=x.test_time,
                test_result=x.test_result
            }).ToList();
            return mid;
        }
    }
}
