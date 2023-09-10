using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using test.Common.Db;
using test.Module.Entities;
using test.Service.Management.UserRouting;
using test.Service.Token;

namespace test.WebAPI.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class GetAllPos_idController : ControllerBase
    {
        public IUserRoutingService _userService;
        public IJWTService _jwtService;
        public GetAllPos_idController(IUserRoutingService userService, IJWTService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }
        /// <summary>
        /// 获取所有pos_id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public List<string> GetAllPos_id()
        {
            List<string> posIds = DbContext.db.Queryable<regioninfo>()
                                .Select(x => x.pos_id)
                                .ToList();
            return posIds;
        }
    }
}
