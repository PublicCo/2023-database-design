﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using test.Common.Db;
using test.Module.Entities;
using test.Service.Token;
using test.Service.Management.RiskLevel;

namespace test.WebAPI.Controllers
{
    
    [Route("[controller]/[Action]")]
    [ApiController]
    public class RiskLevelController : ControllerBase
    {
        public IRiskLevelService _userService;
        public IJWTService _jwtService;
        public RiskLevelController(IRiskLevelService userService, IJWTService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }
        /// <summary>
        /// 获取用户所在地的风险等级，默认用户行程表中一个用户有多个行程
        /// </summary>
        [HttpGet]
        [Authorize]
        public string GetRiskLevel()
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
            int dangerlevel = _userService.GetRiskLevel(id);
            if (dangerlevel == -1)
            {
                data = JsonConvert.SerializeObject(new
                {
                    code = 400,
                    message = "查不到用户行程信息"
                });
                return data;
            }
            data = JsonConvert.SerializeObject(new { code = 200, message = "success", dangerlevel = dangerlevel });
            return data;
        }
        /// <summary>
        /// 获取全国所有地区的风险等级
        /// </summary>
        [HttpGet]
        [Authorize]
        public string SearchRiskLevel()
        {
            List<string> province = new List<string>() {
                "黑龙江省","吉林省","辽宁省","河北省","甘肃省","青海省","陕西省","河南省","山东省","山西省",
                "安徽省","湖北省","湖南省","江苏省","四川省","贵州省","云南省","浙江省","江西省","广东省",
                "福建省","台湾省","海南省","新疆维吾尔自治区","内蒙古自治区","宁夏回族自治区","广西壮族自治区",
                "西藏自治区","北京市","上海市","天津市","重庆市","香港","澳门"
            };
            //表名暂定dangerlevelinfo，后续可以更改
            List<danger_level_info> Dangerlevelinfo = new List<danger_level_info>();
            Dangerlevelinfo = DbContext.db.Queryable<danger_level_info>().ToList();
            //条数
            int nums = Dangerlevelinfo.Count();
            string data;
            data = JsonConvert.SerializeObject(new { code = 200, message = "success", DangerLevelInfo = Dangerlevelinfo });
            return data;
        }
        [HttpPost]
        [Authorize]
        /// <summary>
        /// 超级用户修改地区风险等级
        /// </summary>
        /// <returns></returns>
        public string ModifyDangerLevel(string province,string city,string area,int dangerlevel)
        {
            string data;
            int result = _userService.ModifyDangerLevel(province, city, area, dangerlevel);
            if (result > 0)
            {
                data = JsonConvert.SerializeObject(new { code = 200, message = "success" });
                return data;
            }
            else
            {
                data = JsonConvert.SerializeObject(new { code = 400, message = "数据库原因，抗原上传失败" });
                return data;
            }
        }
    }
}
