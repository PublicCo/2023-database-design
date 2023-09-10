using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Common.Db;
using test.Module.Entities;

namespace test.Service.Management.RiskLevel
{
    public class RiskLevelService: IRiskLevelService
    {
        private readonly IMapper _mapper;
        public RiskLevelService(IMapper mapper)
        {
            _mapper = mapper;
        }
        /// <summary>
        /// 超级用户修改地区风险等级
        /// </summary>
        /// <param name="province"></param>
        /// <param name="city"></param>
        /// <param name="area"></param>
        /// <param name="dangerlevel"></param>
        /// <returns></returns>
        public int ModifyDangerLevel(string province, string city, string area, int dangerlevel)
        {
            List<danger_level_info> dangerlevelinfos = new List<danger_level_info>();
            dangerlevelinfos = DbContext.db.Queryable<danger_level_info>().Where(it => it.province == province
            && it.area == area && it.city == city).ToList();
            if (dangerlevelinfos.Count == 0)
            {
                danger_level_info insertInfo = new danger_level_info();
                insertInfo.province = province;
                insertInfo.city = city;
                insertInfo.area = area;
                insertInfo.danger_Level = dangerlevel;
                int isnert_result = DbContext.db.Insertable(insertInfo).ExecuteCommand();
                if (isnert_result == 0)
                {
                    return -1;
                }
                return isnert_result;
            }
            else
            {
                danger_level_info Dangerlevelinfo = dangerlevelinfos[0];
                DbContext.db.Tracking(Dangerlevelinfo);//创建跟踪
                Dangerlevelinfo.danger_Level = dangerlevel;

                int result = DbContext.db.Updateable(Dangerlevelinfo).ExecuteCommand();
                return result;
            }
        }
        /// <summary>
        /// 查询用户所在地的风险等级的数据库操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetRiskLevel(string id)
        {
            List<user_itinerary> user_Itineraries = new List<user_itinerary>();
            //查询行程
            user_Itineraries = DbContext.db.Queryable<user_itinerary>().Where(it => it.user_id == id).ToList();
            //如果查不到用户行程
            if (user_Itineraries.Count() == 0)
            {
                return -1;
            }
            // 1. 得到最大日期字符串
            var maxDateStr = user_Itineraries
              .Select(x => x.record_time)
              .Max();
            // 2. 根据最大日期字符串查找对象
            var maxItinerary = user_Itineraries
              .First(x => x.record_time == maxDateStr);
            //获取用户最新经过地区的pos_id
            string pos_id = maxItinerary.pos_id;
            //根据该pos_id查询对应区域
            List<regioninfo> regioninfos = new List<regioninfo>();
            regioninfos = DbContext.db.Queryable<regioninfo>().Where(it => it.pos_id == pos_id).ToList();
            //根据区域信息差风险等级表
            List<danger_level_info> dangerlevelinfos = new List<danger_level_info>();
            dangerlevelinfos = DbContext.db.Queryable<danger_level_info>().Where(it => it.province == regioninfos[0].province
            && it.city == regioninfos[0].city
            && it.area == regioninfos[0].area).ToList();

            return dangerlevelinfos[0].danger_Level;
        }
    }
}
