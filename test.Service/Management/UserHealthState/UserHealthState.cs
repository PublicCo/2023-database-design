using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Common.Db;
using test.Module.Entities;
using test.Service.Management.UserRouting;

namespace test.Service.Management.UserHealthState
{
    public class UserHealthState : IUserHealthState
    {
        public void Insert(string id)
        {
            user_health_state data = new user_health_state();
            data.user_id = id;
            data.current_status = "健康";
            data.nucieic_acid_test_result = "无";
            data.vaccination_status = 0;
            if (id == null)
                throw new Exception("id无效");
            int reuslt = DbContext.db.Insertable(data).ExecuteCommand();
            if (reuslt == 0)
                throw new Exception("插入失败");
        }
        public void UpdateCurrentState(string id, string current_state)
        {
            user_health_state data = new user_health_state();
            data.user_id = id;
            data.current_status = current_state;
            int result = DbContext.db.Updateable(data).UpdateColumns(it => new { it.current_status }).Where(it => it.user_id == id).ExecuteCommand();
            if (result == 0)
                throw new Exception("更新失败");
        }
        public void UpdateResult(string id, string nucieic_acid_test_result)
        {
            user_health_state old_data = DbContext.db.Queryable<user_health_state>().Where(it => it.user_id == id).First();
            if (nucieic_acid_test_result == old_data.nucieic_acid_test_result)
            {
                if(old_data.current_status=="密接")
                {
                    UpdateCurrentState(id, "健康");
                }
            }
            else
            {
                if(nucieic_acid_test_result=="阳性")
                {
                    UpdateCurrentState(id, "感染");
                    UpdateTestResult(id, "阳性");
                }
                else
                {
                    UpdateCurrentState(id, "健康");
                    UpdateTestResult(id, "阴性");
                }
            }
        }
        public void UpdateTestResult(string id, string nucieic_acid_test_result)
        {
            user_health_state data = new user_health_state();
            data.user_id = id;
            data.nucieic_acid_test_result = nucieic_acid_test_result;
            int result = DbContext.db.Updateable(data).UpdateColumns(it => new { it.nucieic_acid_test_result }).Where(it => it.user_id == id).ExecuteCommand();
            if (result == 0)
                throw new Exception("更新失败");
        }
        public int UpdateVaccinationStatus(string id)
        {
            user_health_state data = DbContext.db.Queryable<user_health_state>().Where(it => it.user_id == id).First();
            int times = (int)data.vaccination_status++;
            int result = DbContext.db.Updateable(data).UpdateColumns(it => new { it.vaccination_status }).Where(it => it.user_id == id).ExecuteCommand();
            if (result == 0)
                throw new Exception("更新失败");
            return times;
        }
        public List<user_health_state> GetUserHealthInfo(string id)
        {
            List<user_health_state> user_Health_state = DbContext.db.Queryable<user_health_state>()
                        .Where(it => it.user_id == id).ToList();
            return user_Health_state;
        }
        /// <summary>
        /// 查出谁是密接的服务，返回密接者的id列表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sick_time"></param>
        /// <returns></returns>
        public List<string> ContactManagement(string id, DateTime sick_time)
        {
            List<user_itinerary_dto> user_Itinerary_Dtos = new List<user_itinerary_dto>();
            ///获取用户成为阳性前两个小时的所有pos_id和time
            user_Itinerary_Dtos = DbContext.db.Queryable<user_itinerary>()
              .Where(it => it.user_id == id &&
                          it.record_time > sick_time.AddHours(-2) &&
                          it.record_time < sick_time)
              .Select(x => new user_itinerary_dto()
              {
                  pos_id = x.pos_id,
                  time = x.record_time
              })
              .ToList();

            // 1. sick用户行程记录
            var sickItineraries = user_Itinerary_Dtos;

            // 2. 记录所有密接用户id
            List<string> contactIds = new List<string>();

            foreach (var itinerary in sickItineraries)
            {
                // pos_id和时间范围
                var posId = itinerary.pos_id;
                var time = itinerary.time;

                // 3. 查询其他用户
                var others = DbContext.db.Queryable<user_itinerary>()
                  .Where(u => u.user_id != id && // 不包括sick用户自己
                              u.pos_id == posId &&
                              u.record_time > time.AddHours(-1) &&
                              u.record_time < time.AddHours(1))
                  .Select(u => u.user_id)
                  .ToList();

                // 4. 添加到结果集
                contactIds.AddRange(others);
            }
            // 去重 contactIds
            contactIds = contactIds.Distinct().ToList();
            //这时候可能包含一些阳性用户被判定为密接，排除这些人
            // 去除已确诊阳性用户
            List<string> filteredContactIds = new List<string>();
            foreach (var contactId in contactIds)
            {
                var userHealth = DbContext.db.Queryable<user_health_state>()
                  .Where(u => u.user_id == contactId)
                  .Select(u => u.current_status)
                  .First();

                if (userHealth != "阳性")
                {
                    filteredContactIds.Add(contactId);
                }
            }
            return filteredContactIds;
        }
    }
}
