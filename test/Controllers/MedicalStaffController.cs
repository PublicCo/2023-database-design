using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using test.Common.Db;
using test.Service.Token;
using test.Service.Management.MedicalStaff;
using test.Module.Entities;
using SqlSugar;

namespace test.WebAPI.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class MedicalStaffController : ControllerBase
    {
        public IMedicalStaffService _userService;
        public IJWTService _jwtService;
        public MedicalStaffController(IMedicalStaffService userService, IJWTService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }
        /// <summary>
        /// 获取该站点所有工作人员信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public string GetMedicalAllStaff(string hos_id)
        {
            List<workerinfo_dto> workerinfo_Dtos = new List<workerinfo_dto>();
            workerinfo_Dtos = _userService.GetMedicalAllStaff(hos_id);
            string data;
            if (workerinfo_Dtos.Count == 0)
            {
                data = JsonConvert.SerializeObject(new { code = 400, message = "无该站点的工作人员信息" });
                return data;
            }
            else
            {
                data = JsonConvert.SerializeObject(new { code = 200, message = "success", StaffList= workerinfo_Dtos });
                return data;
            }
        }
        /// <summary>
        /// 获取工作人员所在的医疗点id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public string GetStaffMedicalPoint()
        {
            //string token;
            string data;
            /*            try
                        {
                            token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
                        }
                        catch (SecurityTokenExpiredException)
                        {
                            data = JsonConvert.SerializeObject(new { code = 400, message = "用户凭证已过期，请重新登陆" });
                            return data;
                        }
                        catch (Exception)
                        {
                            data = JsonConvert.SerializeObject(new { code = 400, message = "无效令牌签名" });
                            return data;
                        }*/
            //string id = _jwtService.DecodeToken(token);
            string id = _jwtService.GetUserID(HttpContext);
            List<workerinfo_dto2> workerinfo_Dto2s = _userService.GetStaffMedicalPoint(id);
            if (workerinfo_Dto2s.Count == 0)
            {
                data = JsonConvert.SerializeObject(new { code = 400, message = "无该工作人员的工作地点信息" });
                return data;
            }
            else
            {
                data = JsonConvert.SerializeObject(new { code = 200, message = "success", StaffList = workerinfo_Dto2s });
                return data;
            }
        }
        /// <summary>
        /// 工作人员健康登记
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public string StaffPostHealth(string status,DateTime time)
        {
            //string token;
            string data;
            /*            try
                        {
                            token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
                        }
                        catch (SecurityTokenExpiredException)
                        {
                            data = JsonConvert.SerializeObject(new { code = 400, message = "用户凭证已过期，请重新登陆" });
                            return data;
                        }
                        catch (Exception)
                        {
                            data = JsonConvert.SerializeObject(new { code = 400, message = "无效令牌签名" });
                            return data;
                        }*/
            //string id = _jwtService.DecodeToken(token);
            string id = _jwtService.GetUserID(HttpContext);
            int result=_userService.StaffPostHealth(id,status,time);
            if (result>0)
            {
                data = JsonConvert.SerializeObject(new { code = 200, message = "success" });
                return data;
            }
            else
            {
                data = JsonConvert.SerializeObject(new { code = 400, message = "数据库原因，健康登记失败" });
                return data;
            }
        }
        /// <summary>
        /// 政府获取所有工作人员ID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public string GetAllStaffID()
        {
            //string token;
            string data;
            /*            try
                        {
                            token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
                        }
                        catch (SecurityTokenExpiredException)
                        {
                            data = JsonConvert.SerializeObject(new { code = 400, message = "用户凭证已过期，请重新登陆" });
                            return data;
                        }
                        catch (Exception)
                        {
                            data = JsonConvert.SerializeObject(new { code = 400, message = "无效令牌签名" });
                            return data;
                        }*/
            //string id = _jwtService.DecodeToken(token);
            string id = _jwtService.GetUserID(HttpContext);
            List<string> Workerinfo = DbContext.db.Queryable<worker_info>()
                                .Select(x => x.worker_id)
                                .ToList();
            data = JsonConvert.SerializeObject(new { code = 200, message = "success", StaffList= Workerinfo });
            return data;
        }
        [HttpGet]
        [Authorize]
        public ActionResult<worker_health_state> GetWorkerHealth()
        {
            string id = _jwtService.GetUserID(HttpContext);
            return DbContext.db.Queryable<worker_health_state>().OrderBy(it => it.record_time,OrderByType.Desc).First();
        }
    }
}
