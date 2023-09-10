using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using test.Module.Entities;
using test.Service.Management.UserHealthState;
using test.Service.Token;

namespace test.WebAPI.Controllers
{
    /// <summary>
    /// 获取用户健康状态表
    /// </summary>
    [Route("[controller]/[Action]")]
    [ApiController]
    public class UserHealthController : ControllerBase
    {
        public IJWTService _jwtService;
        public IUserHealthState _userHealthState;
        public UserHealthController(IUserHealthState userHealthState, IJWTService jwtService)
        {
            _jwtService = jwtService;
            _userHealthState = userHealthState;
        }
        [HttpGet]
        [Authorize]
        public string GetUserHealthInfo()
        {
            string token;
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
                        }
                        string id = _jwtService.DecodeToken(token);*/
            string id = _jwtService.GetUserID(HttpContext);
            List<user_health_state> user_Health_States = new List<user_health_state>();
            user_Health_States= _userHealthState.GetUserHealthInfo(id);
            data = JsonConvert.SerializeObject(new
            {
                code = 200,
                Message = "success",
                id = user_Health_States[0].user_id,
                current_status = user_Health_States[0].current_status,
                nucieic_acid_test_result = user_Health_States[0].nucieic_acid_test_result,
                vaccination_status= user_Health_States[0].vaccination_status
            });
            return data;
        }
    }
}
