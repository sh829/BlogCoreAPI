using BlogCore.AuthHelper.OverWrite;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogCore.MiddleWares
{
    public static class MiddlewareHelpers
    {
        // 这里定义一个中间件Helper，主要作用就是给当前模块的中间件取一个别名
        public static IApplicationBuilder UseJwtTokenAuth(this IApplicationBuilder app)
        {
            return app.UseMiddleware<JwtTokenAuth>();
        }
        public static IApplicationBuilder UseReuestResponseLog(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequRespLogMildd>();
        }
    }
}
