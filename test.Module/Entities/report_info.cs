using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace test.Module.Entities
{
    /// <summary>
    /// 举报信息
    /// </summary>
    public class report_info
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string? message_id { get; set; }
        public DateTime? message_time { get; set; }
        public string? user_id { get;set; }
        public int? message_state { get; set; }
        public string? message_content { get; set; }
    }
}
