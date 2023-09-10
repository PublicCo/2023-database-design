using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Service.User.Dto
{
    public class InputUserDto
    {
        public string user_id { get; set; }
        public string pwd { get; set; }
        public string user_name { get; set; }
        public string gender { get; set; }
        public string phone_number { get; set; }
        public string user_address { get; set; }
        public string validatekey { get; set; }
        public string validatecode { get; set; }
    }
}
