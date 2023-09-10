using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace test.Module.Entities
{
    /// <summary>
    /// 超级用户表
    /// </summary>
    public class gov_info
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string? gov_id { get; set; }
        public string? province { get; set; }
        public string? city { get; set; }
        public string? area { get; set; }
        public string? pwd { get; set; }
        public string? phone_number { get; set; }
    }
}
