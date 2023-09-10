using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Service.Management.VaccineInfo
{
    public class vaccinate_record
    {
        public string? vaccin_id { get; set; }
        public string? vaccinate_time { get; set; }
        public int vaccinate_times { get; set; }
        public string? manufacture { get; set; }
        public string? type { get; set; }
    }
}
