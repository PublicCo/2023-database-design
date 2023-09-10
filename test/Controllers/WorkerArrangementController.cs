using Microsoft.AspNetCore.Mvc;
using test.Service.Management.WorkArrange;
using test.Service.Token;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace test.Module.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class WorkerArrangementController: ControllerBase
    {
        private readonly IWAInterface _wainterface;
        private readonly IJWTService _jwtService;
        public WorkerArrangementController(IWAInterface wAInterface,IJWTService jWTService)
        {
            _wainterface = wAInterface;
            _jwtService = jWTService;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<object> GovermentArrangeWork(string staff_id, string pos_id, string ddl_time)
        {
            /*            string token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
                        string id = _jwtService.DecodeToken(token);*/
            string id = _jwtService.GetUserID(HttpContext);
            var result = _wainterface.GovSetArrangement(id, staff_id, pos_id, ddl_time);
            if(result == true)
            {
                var data = JsonConvert.SerializeObject(new
                {
                    code = 200,
                    message="success"
                });
                return Ok(data);
            }
            else
            {
                var data = JsonConvert.SerializeObject(new
                {
                    code = "400",
                    message = "fail to set work"
                });
                return BadRequest(data);
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult<object> GetWork()
        {
            /*            string token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
                        string id = _jwtService.DecodeToken(token);*/
            string id = _jwtService.GetUserID(HttpContext);
            List<StaffWorkStatus> returndata = _wainterface.StaffGetWork(id);
            return returndata;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<object> PostWork([FromBody] StaffWorkStatus staffWorkStatus)
        {
            /*            string token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
                        string id = _jwtService.DecodeToken(token);*/
            string id = _jwtService.GetUserID(HttpContext);
            var result = _wainterface.StaffPostWork(id, staffWorkStatus.manager_id, staffWorkStatus.pos_id, staffWorkStatus.ddl_time, staffWorkStatus.donetime);
            if (result == true)
            {
                var data = JsonConvert.SerializeObject(new
                {
                    code = 200,
                    message = "success"
                });
                return Ok(data);
            }
            else
            {
                var data = JsonConvert.SerializeObject(new
                {
                    code = 400,
                    message = "fail to set work done"
                });
                return BadRequest(data);
            }
        }

    }
}
