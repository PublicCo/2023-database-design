using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace test.Module.Entities
{
    /// <summary>
    /// 普通用户表
    /// </summary>
    public class user_info
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string? user_id { get; set; }
        public string? user_name { get; set; }
        public string? gender { get; set; }
        public string? pwd { get; set; }
        public string? phone_number { get; set; }
        public string? user_address { get; set; }
    }
}
