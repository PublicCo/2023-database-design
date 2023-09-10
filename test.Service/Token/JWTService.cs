using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using test.Module.Entities;
using test.Module.Enum;

namespace test.Service.Token
{
    public class JWTService : IJWTService
    {
        private readonly JWTToken _JWTToken;
        public JWTService(IOptionsMonitor<JWTToken> jwttoken)
        {
            _JWTToken = jwttoken.CurrentValue;
        }
        private Claim[] GetClaims(Object user, int userType)
        {
            switch (userType)
            {
                case (int)user_type.USER:
                    {
                        user_info tokenUser = (user_info)user;
                        var claims = new[]
                        {
                            new Claim("user_type",userType.ToString()),
                            new Claim("id",tokenUser.user_id),
                            new Claim("name",tokenUser.user_name),
                            new Claim("phone_number",tokenUser.phone_number),
                            new Claim("address",tokenUser.user_address),
                        };
                        return claims;
                    }
                case (int)user_type.WORKER:
                    {
                        worker_info tokenUser = (worker_info)user;
                        var claims = new[]
                        {
                            new Claim("user_type",userType.ToString()),
                            new Claim("id",tokenUser.worker_id),
                            new Claim("pos_id",tokenUser.pos_id),
                            new Claim("hos_id",tokenUser.hos_id),
                            new Claim("phone_number",tokenUser.phone_number),
                        };
                        return claims;
                    }
                case (int)user_type.GOV:
                    {
                        gov_info tokenUser = (gov_info)user;
                        var claims = new[]
                        {
                            new Claim("user_type",userType.ToString()),
                            new Claim("id",tokenUser.gov_id),
                            new Claim("province",tokenUser.province),
                            new Claim("city",tokenUser.city),
                            new Claim("area",tokenUser.area),
                            new Claim("phone_number",tokenUser.phone_number),
                        };
                        return claims;
                    }
            }
            throw new Exception("无对应用户类型");
        }
        public string GetToken(Object user, int userType)
        {
            var claims = GetClaims(user, userType);

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTToken.SecurityKey));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken
                (
                issuer: _JWTToken.Issuer,
                audience: _JWTToken.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public string DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_JWTToken.SecurityKey);

            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _JWTToken.Issuer,

                ValidateAudience = true,
                ValidAudience = _JWTToken.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),

                ClockSkew = TimeSpan.Zero // No clock skew
            };

            SecurityToken validatedToken;
            ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

            // 获取用户的 ID
            var userIdClaim = claimsPrincipal.FindFirst("id");
            if (userIdClaim != null)
            {
                string userId = userIdClaim.Value;
                return userId;
            }
            else
            {
                return ""; // 用户 ID 未找到
            }
        }
        public int GetUserType(HttpContext httpContext)
        {
            return Convert.ToInt32(httpContext.User.Claims.ToList()[0].Value);
        }
        public string GetUserID(HttpContext httpContext)
        {
            return Convert.ToString(httpContext.User.Claims.ToList()[1].Value);
        }
    }
}
