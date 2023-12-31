﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace test.Module.Entities
{
    /// <summary>
    /// 感染人数信息
    /// </summary>
    public class infection_info
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string? pos_id { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public DateTime record_time { get; set; }
        public int infected_number { get; set; }
    }
}
