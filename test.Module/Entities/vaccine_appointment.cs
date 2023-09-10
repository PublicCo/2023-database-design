using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace test.Module.Entities
{
    /// <summary>
    /// 疫苗预约
    /// </summary>
    public class vaccine_appointment
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string? hos_id { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public DateTime appoint_time { get; set; }
        public string? appoint_num { get; set; }
        public string? appoint_state { get; set; }
    }
}
