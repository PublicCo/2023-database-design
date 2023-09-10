using Microsoft.AspNetCore.Mvc;
using test.Module.Enum;
using test.Service.Management.UserHealthState;
using test.Service.Token;
using test.Service.User;
using test.Service.User.Dto;

namespace test.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService;
        public IJWTService _jwtService;
        public IUserHealthState _userHealthState;
        public UserController(IUserService userService, IJWTService jwtService, IUserHealthState userHealthState)
        {
            _userService = userService;
            _jwtService = jwtService;
            _userHealthState = userHealthState;
        }
        [HttpPost]
        public ActionResult<string> Regist( InputUserDto input)
        {
            try
            {
                var res = _userService.AddUser(input);
                _userHealthState.Insert(input.user_id);
                return _jwtService.GetToken(res, (int)user_type.USER);
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