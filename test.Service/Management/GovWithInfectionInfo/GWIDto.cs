using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.Service.Management.GovWithInfectionInfo
{
    public enum  CheckType { Province = 0, City = 1, Region = 2, };
    public class InfectPeopleNumber
    {
        public string pos_name { get; set; }
        public int number { get; set; }
    }
    public class DayInfectNumber
    {
        public DateTime SelectDate { get; set; }
        public int number { get; set; }
    }
}
