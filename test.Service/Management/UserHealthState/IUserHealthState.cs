using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.Module.Entities;

namespace test.Service.Management.UserHealthState
{
    public interface IUserHealthState
    {
        void Insert(string id);
        void UpdateCurrentState(string id, string current_state);
        void UpdateResult(string id, string nucieic_acid_test_result);
        void UpdateTestResult(string id, string nucieic_acid_test_result);
        List<user_health_state> GetUserHealthInfo(string id);
        List<string> ContactManagement(string id, DateTime sick_time);
        public int UpdateVaccinationStatus(string id);
    }
}
