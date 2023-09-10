using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Service.Management.VaccineInfo
{
    public class hos_vaccine_insert_dto
    {
        public string? vaccine_id { get; set; }
        public string? medical_point_id { get; set; }
        public int is_safe { get; set; }
        public string? vaccine_type_id { get; set; }
        public string? production_date { get; set; }
    }
}
