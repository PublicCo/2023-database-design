using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SqlSugar;
using test.Common.Db;
using test.Module.Entities;
using test.Service.Token;
using test.Service.User;

namespace test.WebAPI.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class UpdateInfectorController : ControllerBase
    {
        public IUserService _userService;
        public IJWTService _jwtService;
        public UpdateInfectorController(IUserService userService, IJWTService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }
        /// <summary>
        /// 更新感染人数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[Authorize]

        public string UpdateInfector(string date)
        {
            ///从regioninfo表查询所有pos_id,生成一个PosId-InfectedNumber的字典
            var posDict = DbContext.db.Queryable<regioninfo>()
                .Select(x => new { x.pos_id, InfectedNumber = 0 })
                .ToList()
                .ToDictionary(x => x.pos_id, x => Convert.ToInt32(x.InfectedNumber));
            //查询每个用户最新行程

            var latestTrips = DbContext.db.Queryable<user_itinerary>()
                                .OrderBy(it => it.record_time, OrderByType.Desc)
                                .GroupBy(it => new { it.user_id, it.pos_id })
                                .Select(it => new user_itinerary { user_id = it.user_id,pos_id=it.pos_id, record_time = SqlFunc.AggregateMax(it.record_time) })
                                .ToList();
            //统计
            foreach (var trip in latestTrips)
            {
                var userId = trip.user_id;
                var posId = trip.pos_id;

                if (DbContext.db.Queryable<user_health_state>().First(u => u.user_id == userId).current_status == "感染")
                {
                    posDict[posId]++;
                }
            }
            List<infection_info> addlist = new List<infection_info>();
            //插入
            foreach (var item in posDict)
            {
                try
                {
                    addlist.Add(new infection_info
                    {
                        pos_id = item.Key,
                        record_time = Convert.ToDateTime(date),
                        infected_number = (int)item.Value
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("错误信息：" + ex.ToString());
                }

            }

            //(3)、大数据写入（特色功能：大数据处理上比所有框架都要快30%）
            //优点：1000条以上性能无敌手
            //缺点：不支持数据库默认值， API功能简单， 小数据量并发执行不如普通插入，插入数据越大越适合用这个
            //新功能 5.0.44
            DbContext.db.Fastest<infection_info>().PageSize(100000).BulkCopy(addlist);
            /*foreach (var item in posDict)
            {
                try
                {
                    DbContext.db.Insertable<infection_info>(new infection_info
                    {
                        pos_id = item.Key,
                        record_time = date,
                        infected_number = (int)item.Value
                    }).ExecuteCommand();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("错误信息：" + ex.ToString());
                }

            }*/
            return JsonConvert.SerializeObject(new { code = 200, message = "success" });
        }
    }
}
