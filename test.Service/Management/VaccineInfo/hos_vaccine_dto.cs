using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Service.Management.VaccineInfo
{
    public class hos_vaccine_dto
    {
        public string? vaccine_type_id { get; set; }
        public string? manufacture { get; set; }
        public string? type_description { get; set; }
        public int save_time { get; set; }
        public string? vaccine_id { get; set; }
        public DateTime? production_date { get; set; }
    }
}
