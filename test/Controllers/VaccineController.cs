using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using test.Module.Entities;
using test.Module.Enum;
using test.Service.Management.VaccineInfo;
using test.Service.Token;

namespace test.WebAPI.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class VaccineController : ControllerBase
    {
        public IVaccineInfo _vaccine;
        public IJWTService _jwtService;
        public VaccineController(IVaccineInfo vaccine, IJWTService jWTService)
        {
            _vaccine = vaccine;
            _jwtService = jWTService;
        }
        [HttpPost]
        [Authorize]
        public ActionResult<int> ApplyforVaccine(string hos_id,string number)
        {
            try
            {
                int userType = _jwtService.GetUserType(HttpContext);
                if (userType == (int)user_type.USER)
                {
                    throw new Exception("无权限");
                }
                appoint_insert_dto input = new appoint_insert_dto();
                input.appoint_num = number;
                input.hos_id = hos_id;
                int result = _vaccine.InsertVaccineAppointRequest(input);
                if (result != 0)
                {
                    return result;
                }
                else
                {
                    throw new Exception("申请失败");
                }
            }
            catch (Exception ex)
            {
                JsonResult jresult = new JsonResult(ex.Message)
                {
                    StatusCode = 500
                };
                return jresult;
            }
        }
        [HttpGet]
        [Authorize]
        public ActionResult<List<vaccine_appointment>>GetNotHandledAppointmen()
        {
            try
            {
                int userType = _jwtService.GetUserType(HttpContext);
                if (userType == (int)user_type.USER)
                {
                    throw new Exception("无权限");
                }
                return _vaccine.GetVaccineAppointment();
            }
            catch (Exception ex)
            {
                JsonResult jresult = new JsonResult(ex.Message)
                {
                    StatusCode = 500
                };
                return jresult;
            }
        }
        [HttpPut]
        [Authorize]
        public ActionResult<int> HandleAppointment(string hos_id,string status)
        {
            try
            {
                int userType = _jwtService.GetUserType(HttpContext);
                if (userType == (int)user_type.USER)
                {
                    throw new Exception("无权限");
                }
                appoint_update_dto input = new appoint_update_dto();
                input.appoint_state = status;
                input.hos_id = hos_id;
                return _vaccine.UpdateVaccienAppointment(input);
            }
            catch (Exception ex)
            {
                JsonResult jresult = new JsonResult(ex.Message)
                {
                    StatusCode = 500
                };
                return jresult;
            }
        }
        [HttpGet]
        [Authorize]
        public ActionResult<List<hos_vaccine_result_dto>>GetHosVaccine(string hos_id)
        {
            try
            {
                int userType = _jwtService.GetUserType(HttpContext);
                if (userType == (int)user_type.USER)
                {
                    throw new Exception("无权限");
                }
                return _vaccine.GetHosVaccineInfo(hos_id);
            }
            catch (Exception ex)
            {
                JsonResult jresult = new JsonResult(ex.Message)
                {
                    StatusCode = 500
                };
                return jresult;
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult<int> InsertHosVaccine([FromBody] hos_vaccine_insert_dto input)
        {
            try
            {
                int userType = _jwtService.GetUserType(HttpContext);
                if (userType == (int)user_type.USER)
                {
                    throw new Exception("无权限");
                }
                return _vaccine.InsertHosVaccine(input);
            }
            catch (Exception ex)
            {
                JsonResult jresult = new JsonResult(ex.Message)
                {
                    StatusCode = 500
                };
                return jresult;
            }
        }
        [HttpPut]
        [Authorize]
        public ActionResult<int> WorkerVaccinate(string vaccine_id, string vaccine_time)
        {
            try
            {
                int userType = _jwtService.GetUserType(HttpContext);
                if (userType == (int)user_type.USER)
                {
                    throw new Exception("无权限");
                }
                int result = _vaccine.UpdateVaccineStatus(vaccine_id, vaccine_time);
                if(result == 0)
                {
                    throw new Exception("更新失败，无此疫苗预约信息");
                }
                return result;
            }
            catch (Exception ex)
            {
                JsonResult jresult = new JsonResult(ex.Message)
                {
                    StatusCode = 500
                };
                return jresult;
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult<int> UserVaccinate(string user_id, string vaccine_id)
        {
            try
            {
                int userType = _jwtService.GetUserType(HttpContext);
                if (userType == (int)user_type.USER)
                {
                    throw new Exception("无权限");
                }
                return _vaccine.InsertVaccinateRecord(user_id, vaccine_id);
            }
            catch (Exception ex)
            {
                JsonResult jresult = new JsonResult(ex.Message)
                {
                    StatusCode = 500
                };
                return jresult;
            }
        }
        [HttpGet]
        [Authorize]
        public ActionResult<List<vaccinate_record>> GetVaccinateRecords()
        {
            try
            {
                string user_id = _jwtService.GetUserID(HttpContext);
                if (user_id == null)
                {
                    throw new Exception("获取id出错");
                }
                return _vaccine.GetVaccinateRecord(user_id);
            }
            catch (Exception ex)
            {
                JsonResult jresult = new JsonResult(ex.Message)
                {
                    StatusCode = 500
                };
                return jresult;
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult<int> DestroyVaccine(string id, int is_safe)
        {
            try
            {
                int userType = _jwtService.GetUserType(HttpContext);
                if (userType == (int)user_type.USER)
                {
                    throw new Exception("无权限");
                }
                one_vaccine_info data = new one_vaccine_info();
                data.vaccine_id = id;
                data.is_safe = is_safe;
                return _vaccine.UpddateVaccineSafeStatus(data);
            }
            catch (Exception ex)
            {
                JsonResult jresult = new JsonResult(ex.Message)
                {
                    StatusCode = 500
                };
                return jresult;
            }
        }
        [HttpGet]
        [Authorize]
        public ActionResult<List<vaccine_info>> GetAllVaccineType()
        {
            try
            {
                int userType = _jwtService.GetUserType(HttpContext);
                if (userType == (int)user_type.USER)
                {
                    throw new Exception("无权限");
                }
                return _vaccine.GetVaccineType();
            }
            catch (Exception ex)
            {
                JsonResult jresult = new JsonResult(ex.Message)
                {
                    StatusCode = 500
                };
                return jresult;
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult<string> AddVaccineType(vaccine_info data)
        {
            try
            {
                int userType = _jwtService.GetUserType(HttpContext);
                if (userType == (int)user_type.USER)
                {
                    throw new Exception("无权限");
                }
                return _vaccine.InsertVaccineType(data);
            }
            catch (Exception ex)
            {
                JsonResult jresult = new JsonResult(ex.Message)
                {
                    StatusCode = 500
                };
                return jresult;
            }
        }
        [HttpGet]
        [Authorize]
        public ActionResult<List<apply_result_dto>> GetApplyHistory(string hos_id)
        {
            try
            {
                int userType = _jwtService.GetUserType(HttpContext);
                if (userType == (int)user_type.USER)
                {
                    throw new Exception("无权限");
                }
                return _vaccine.GetAppointmentHistory(hos_id);
            }
            catch (Exception ex)
            {
                JsonResult jresult = new JsonResult(ex.Message)
                {
                    StatusCode = 500
                };
                return jresult;
            }
        }
    }
}
