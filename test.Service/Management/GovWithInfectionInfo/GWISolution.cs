using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Common.Db;
using test.Module.Entities;
using SqlSugar;
namespace test.Service.Management.GovWithInfectionInfo
{
    public class GWISolution : IGWIInterface
    {
        public List<DayInfectNumber> GetDayInfectNumbers(string province, string city, string area)
        {
            try
            {
                var result = DbContext.db.Queryable<regioninfo>()
               .LeftJoin<infection_info>((t1, t2) => t1.pos_id == t2.pos_id)
               .Where((t1, t2) => t1.province == province && t1.city == city&&t1.area==null)
               .Select((t1, t2) => new DayInfectNumber
               {
                   SelectDate = t2.record_time,
                   number = t2.infected_number,

               })
               .ToList();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                return null;
            }
           
        }

        public List<InfectPeopleNumber> GetInfectPeopleNumbers(CheckType checkType,DateTime time)
        {
            if (checkType == CheckType.Province)
            {
                try
                {
                    var result = DbContext.db.Queryable<regioninfo>()
                    .LeftJoin<infection_info>((t1, t2) => t1.pos_id == t2.pos_id)
                    .Where((t1, t2) => t2.record_time.Date == time.Date)
                    .Select((t1, t2) => new InfectPeopleNumber
                    {
                        pos_name = t1.province,
                        number = t2.infected_number
                    })
                    .ToList();

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error:" + ex.Message);
                    return null;
                }
            }
            else if (checkType == CheckType.City)
            {
                try
                {
                    var result = new List<InfectPeopleNumber>();
                    var test = DbContext.db.Queryable<regioninfo>().ToList();
                    var test2 = DbContext.db.Queryable<infection_info>().ToList();
                    foreach (var infectnum in test2)
                    {
                        if (infectnum.record_time.Date == time.Date)
                        {
                            foreach (var rg_info in test)
                            {
                                if (rg_info.pos_id == infectnum.pos_id)
                                {
                                    int my = 0;
                                    if (rg_info.city != null)
                                    {
                                        result.Add(new InfectPeopleNumber
                                        {
                                            number = infectnum.infected_number,
                                            pos_name = rg_info.city
                                        });
                                    }

                                }
                            }
                        }
                    }


                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error:" + ex.Message);
                    return null;
                }
            }
            else if (checkType == CheckType.Region)
            {
                try
                {
                    var result = DbContext.db.Queryable<regioninfo>()
                    .LeftJoin<infection_info>((t1, t2) => t1.pos_id == t2.pos_id)
                    .Where((t1, t2) => t2.record_time.Date == time.Date)
                    .Select((t1, t2) => new InfectPeopleNumber
                    {
                        pos_name = t1.area,
                        number = t2.infected_number
                    })
                    .ToList();
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error:" + ex.Message);
                    return null;
                }
            }
            return null;
        }
    }
}
