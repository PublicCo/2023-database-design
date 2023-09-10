using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using test.Common;
using test.Service.Token;
using test.Service.User;
using test.Service.User.Dto;

namespace test.WebAPI.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        public IUserService _userService;
        public IJWTService _jwtService;
        public LoginController(IUserService userService, IJWTService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }
        [HttpGet]
        public IActionResult GetValidateCodeImages(string t)
        {
            var validateCodeString = Tools.CreateValidateString();

            MemoryHelper.SetMemory(t, validateCodeString, 1);

            byte[] buffer = Tools.CreateValidateCodeBuffer(validateCodeString);
            return File(buffer, @"image/jpeg");
        }

        [HttpGet("{id}/{pwd}")]
        public ActionResult<object> CheckLogin(string id, string pwd, string validateKey, string validateCode, int userType)
        {
            try
            {
                LoginResultDto resultDto = new LoginResultDto();
                var currCode = MemoryHelper.GetMemory(validateKey);
                if (currCode == null)
                    throw new Exception("验证码错误");
                if (currCode.ToString() == validateCode)
                {
                    LoginDto loginDto = new LoginDto();
                    loginDto.id = id;
                    loginDto.pwd = pwd;
                    loginDto.userType = userType;
                    var user = _userService.CheckLogin(loginDto).Result;
                    if (user != null)
                    {
                        string token = _jwtService.GetToken(user, userType);
                        resultDto.token = token;
                        resultDto.user = user;
                        return JsonConvert.SerializeObject(new { resultDto = resultDto });
                        //return resultDto;
                    }
                    else
                        throw new Exception("登录失败");
                }
                else
                    throw new Exception("验证码错误");
            }
            catch (Exception ex)
            {
                JsonResult result = new JsonResult(ex.Message)
                {
                    StatusCode = 500
                };
                return result;
            }
        }
    }
}
