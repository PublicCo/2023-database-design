using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace test.Service.Management.Antigen
{
    /// <summary>
    /// 工作人员dto
    /// </summary>
    public class antigen_record_dto
    {
        public DateTime test_time { get; set; }
        public string? test_result { get; set; }
    }
}
