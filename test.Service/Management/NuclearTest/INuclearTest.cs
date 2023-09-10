using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Module.Entities;

namespace test.Service.Management.NuclearTest
{
    public interface INuclearTest
    {
        public nuclear_test_info Insert(nuclear_test_info_dto input);
        public string UpdateDetectionResult(nuclear_detection_dto input);
        public string UpdateDetectionResult(List<nuclear_detection_dto> input);
        List<nuclear_detection_result_dto> GetDetectionResultById(string id);
        List<nuclear_infected_dto> GetInfectdPeople(DateTime start, DateTime end);
        nuclear_test_info GetInfoByTube(string test_tube_ID);
    }
}
