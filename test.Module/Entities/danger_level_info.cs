using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Module.Entities
{
    public class danger_level_info
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string province { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public string city { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public string area { get; set; }
        public int danger_Level { get; set; }
    }
}
