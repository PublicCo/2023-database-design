using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using test.Service.Management.Notice;
using test.Service.User;
using test.Service.Token;
using Newtonsoft.Json;

namespace test.WebAPI.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class NoticeController:ControllerBase
    {
        private readonly ICreateNotice _createNotice;
        private readonly IUpdateNotice _updateNotice;
        private readonly IUserService _userService;
        private readonly IJWTService _jwtService;
        public NoticeController(ICreateNotice createNotice,IUpdateNotice updateNotice,
                                IUserService userService,IJWTService jwtService)
        {
            _createNotice = createNotice;
            _updateNotice = updateNotice;
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<object> CreateNotice([FromBody] NoticeDto input)
        {
            if (input == null)
            {
                return BadRequest("NullInput");
            }
            var notice = new NoticeDto
            {
                content = input.content,
                name = input.name,
                time = input.time,

            };
            //string token;
            //token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            //string id = _jwtService.DecodeToken(token);
            string id = _jwtService.GetUserID(HttpContext);
            var result = _createNotice.CreateNotice(notice,id);
            if(result==true)
                return Ok("success");
            else
                return BadRequest("fail to insert notice");
        }

        [HttpPost]
        [Authorize]
        public ActionResult<object> UpdateNotice(int notice_id,string notice_name,string content,string time)
        {
            UpdateDto input=new UpdateDto();
            input.content = content;
            input.time=Convert.ToDateTime(time);
            input.notice_id = notice_id;
            input.notice_name = notice_name;
            if (input == null)
            {
                return BadRequest("Null Input");
            }
            var update_notice = new UpdateDto
            {
                notice_id = input.notice_id,
                notice_name = input.notice_name,
                content = input.content,
                time = input.time,
            };
            var result = _updateNotice.UpdateNotice(update_notice);
            if (result == true)
                return Ok("Success");
            else
                return BadRequest("Fail to Update");
        }

        [HttpGet]
        [Authorize]
        public ActionResult<object> GetNotice()
        {
            var data = _updateNotice.GetAllNotice();
            if(data == null)
            {
                return JsonConvert.SerializeObject(new
                {
                    code = 400,
                    message = "fail to get",
                    MessageDetail = data
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    code = 200,
                    message = "success",
                    MessageDetail = data
                });
            }
        }
    }

}
