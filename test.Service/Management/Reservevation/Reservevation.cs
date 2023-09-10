using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Common.Db;
using test.Module.Entities;
using test.Service.Management.VaccineInfo;

namespace test.Service.Management.Reservevation
{
    public class ReservevationService: IReservevationService
    {
        private readonly IMapper _mapper;
        public ReservevationService(IMapper mapper)
        {
            _mapper = mapper;
        }
        /// <summary>
        /// 用户预约的数据库操作
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="hos_name"></param>
        /// <param name="type"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public int Reservevation(string user_id, string hos_name, int type, DateTime time)
        {
            ///先获取预约表最后一行id的值（最新值）
            int newest = DbContext.db.Queryable<appointment_info>().Count();
            newest+=2;

            
            //根据hos_name找到hos_id
            List<string> hos_ids = DbContext.db.Queryable<hospital_info>().Where(it => it.hos_name == hos_name).Select(it => it.hos_id).ToList();
            //如果找不到
            if (hos_ids.Count == 0)
            {
                return -1;
            }
            string hos_id = hos_ids[0];
            appointment_info Appointment_info = new appointment_info()
            {
                appointment_id = newest.ToString(),
                user_id = user_id,
                hos_id = hos_id,
                appoint_time = time,
                appoint_type = type,
                appoint_state = 0
            };
            ///返回受影响条数
            return DbContext.db.Insertable(Appointment_info).ExecuteCommand();
        }
        /// <summary>
        /// 获取预约信息
        /// </summary>
        /// <param name="hos_id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<appointment_result_dto> GetUserReservation(string hos_id, int type)
        {
            List<appointment_info_dto> appointment_Info_Dtos = new List<appointment_info_dto>();
            appointment_Info_Dtos = DbContext.db.Queryable<appointment_info>().Where(it => it.hos_id == hos_id && it.appoint_type == type && it.appoint_state == 0).Select(x => new appointment_info_dto()
            {
                user_id = x.user_id,
                time = x.appoint_time,
                state = x.appoint_state
            }).ToList();
            List<appointment_result_dto> result = _mapper.Map<List<appointment_info_dto>, List<appointment_result_dto>>(appointment_Info_Dtos);
            for(int i=0;i<result.Count;i++)
            {
                result[i].time = appointment_Info_Dtos[i].time.Value.Date.ToShortDateString();
            }
            return result;
        }
        /// <summary>
        /// 修改预约信息
        /// </summary>
        /// <param name="hos_id"></param>
        /// <param name="time"></param>
        /// <param name="user_id"></param>
        /// <param name="type"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int ModifyReservation(string hos_id, DateTime time, string user_id, int type, int state)
        {
            List<appointment_info> appointment_Infos = new List<appointment_info>();
            appointment_Infos = DbContext.db.Queryable<appointment_info>().Where(it => it.hos_id == hos_id
            && it.appoint_type == type && it.user_id == user_id && it.appoint_time == time).ToList();
            if (appointment_Infos.Count > 0)
            {
                appointment_info appointment_Info = appointment_Infos[0];
                DbContext.db.Tracking(appointment_Info);//创建跟踪
                appointment_Info.appoint_state = state;
                int result = DbContext.db.Updateable(appointment_Info).ExecuteCommand();
                return result;
            }
            else
            {
                return -1;
            }
        }
        /// <summary>
        /// 用户取消预约
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int CancelReservevation(string id,int type)
        {
            List<appointment_info> appointment_Info= DbContext.db.Queryable<appointment_info>().Where(it=>it.user_id==id && it.appoint_type == type&&it.appoint_state == 0).ToList();
            //取最新的预约信息
            if(appointment_Info.Count == 0)
            {
                return -1;
            }
            appointment_info earliestAppointment = appointment_Info.OrderBy(a => a.appoint_time).First();
            int result=DbContext.db.Deleteable<appointment_info>(earliestAppointment).ExecuteCommand();
            return result;
        }
    }
}
