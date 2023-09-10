using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using test.Module.Entities;

namespace test.Service.Token
{
    public interface IJWTService
    {
        string GetToken(Object user, int userType);
        //string DecodeToken(string token);
        int GetUserType(HttpContext httpContext);
        string GetUserID(HttpContext httpContext);
    }
}
