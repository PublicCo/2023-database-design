using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using test.Module.Entities;
using test.Module.Enum;
using test.Service.Management.NuclearTest;
using test.Service.Management.UserHealthState;
using test.Service.Token;

namespace test.WebAPI.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class NuclearTestController : ControllerBase
    {
        public INuclearTest _nuclearTest;
        public IJWTService _jwtService;
        public IUserHealthState _userHealthState;

        public NuclearTestController(INuclearTest nuclearTest, IJWTService jwtService, IUserHealthState userHealthState)
        {
            _nuclearTest = nuclearTest;
            _jwtService = jwtService;
            _userHealthState = userHealthState;
        }

        [HttpPost("{user_id}/{pipe_id}")]
        [Authorize]
        public ActionResult<nuclear_test_info> InsertNuclearTestInfo(string user_id,string pipe_id)
        {
            try
            {
                int userType = _jwtService.GetUserType(HttpContext);
                if (userType == (int)user_type.USER)
                {
                    throw new Exception("非法访问");
                }
                nuclear_test_info_dto input = new nuclear_test_info_dto();
                input.user_id = user_id;
                input.test_tube_ID = pipe_id;
                var result = _nuclearTest.Insert(input);
                return result;
            }
            catch(Exception ex)
            {
                JsonResult result = new JsonResult(ex.Message)
                {
                    StatusCode = 500
                };
                return result;
            }
        }
        [HttpPut]
        [Authorize]
        public ActionResult<string> UpdateTestResult(string result, string test_tube_id)
        {
            try
            {
                int userType = _jwtService.GetUserType(HttpContext);
                if (userType == (int)user_type.USER)
                {
                    throw new Exception("非法访问");
                }
                nuclear_detection_dto input = new nuclear_detection_dto();
                input.test_tube_ID = test_tube_id;
                input.test_result = result;
                _nuclearTest.UpdateDetectionResult(input);
                nuclear_test_info infected = _nuclearTest.GetInfoByTube(test_tube_id);
                if (result == "阳性")
                {
                    List<string> contact_ids = _userHealthState.ContactManagement(infected.user_id, infected.test_time);
                    foreach(string contact_id in contact_ids)
                    {
                        _userHealthState.UpdateCurrentState(contact_id, "密接");
                    }
                }
                _userHealthState.UpdateResult(infected.user_id, result);
                return "1";
            }
            catch (Exception ex)
            {
                JsonResult res = new JsonResult(ex.Message)
                {
                    StatusCode = 500
                };
                return res;
            }
        }
        [HttpGet]
        [Authorize]
        public List<nuclear_detection_result_dto> GetTestInfoByID()
        {
            string id = _jwtService.GetUserID(HttpContext);
            List<nuclear_detection_result_dto> result=_nuclearTest.GetDetectionResultById(id);
            return result;
        }
        [HttpGet("id")]
        [Authorize]
        public List<nuclear_detection_result_dto> GetTestInfoByID(string id)
        {
            List<nuclear_detection_result_dto> result = _nuclearTest.GetDetectionResultById(id);
            return result;
        }
        [HttpGet]
        [Authorize]
        public ActionResult<List<nuclear_infected_dto>>GetAllInfectedPeople()
        {
            int userType = _jwtService.GetUserType(HttpContext);
            if (userType == (int)user_type.USER)
            {
                JsonResult res = new JsonResult("无权限")
                {
                    StatusCode = 500
                };
                return res;
            }
            string start = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            string end = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            DateTime dtstart=Convert.ToDateTime(start);
            DateTime dtend=Convert.ToDateTime(end);
            return _nuclearTest.GetInfectdPeople(dtstart, dtend);
        }
    }
}
