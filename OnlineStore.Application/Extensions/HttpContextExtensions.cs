using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserIp(this HttpContext httpContext)
       => httpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    }
}
