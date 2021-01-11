using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SehirRehberi.API.Helpers
{
    public static class JwtExtension
    {
        public static void AddApplicationError(this HttpResponse response,string message)
        {
            response.Headers.Add("Application-error" , message);
            response.Headers.Add("Access-Conrol-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Expose-Header", "Application-Error");
            response.Headers.Add("Access - Control - Allow - Headers","Origin,X-Requested-With,Content-Type,Accept,content-type,application/json");
            response.Headers.Add("Access-Control-Allow-Methods", "GET,PUT,POST,DELETE,OPTIONS");
        }
    }
}
