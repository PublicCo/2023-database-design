using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace test.Module.Entities
{
    /// <summary>
    /// 公告信息
    /// </summary>
    public class notice_info
    {
        public DateTime? notice_time { get; set; }
        public string? notice_name { get; set; }
        public string? notice_content { get; set; }
        public string? super_id { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public int notice_id { get; set; }
    }
}
