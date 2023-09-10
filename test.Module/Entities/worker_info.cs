using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace test.Module.Entities
{
    /// <summary>
    /// 工作人员
    /// </summary>
    public class worker_info
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string? worker_id { get; set; }
        public string? pos_id { get; set; }
        public string? hos_id { get; set; }
        public string? pwd { get; set; }
        public string? phone_number { get; set; }
    }
}
