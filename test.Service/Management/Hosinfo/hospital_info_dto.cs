using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace test.Service.Management.HosInfo
{
    /// <summary>
    /// 工作人员dto
    /// </summary>
    public class hospital_info_dto
    {
        public string? name { get; set; }
        public int? max_num_of_people { get; set; }
        public int? current_num_of_people { get; set; }
        public string? pos_id { get; set; }
    }
}
