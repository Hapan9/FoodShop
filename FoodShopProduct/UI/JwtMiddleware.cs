using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Hosting;

namespace UI
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAuthService authService, IWebHostEnvironment web)
        {
            var isAuthorizeAttribute = context.Features.Get<IEndpointFeature>().Endpoint?.Metadata
                .GetMetadata<CustomAuthorizeAttribute>() != null;

            if (web.IsProduction() && isAuthorizeAttribute)
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (await authService.IsTokenValid(token))
                {
                    await _next(context);

                    return;
                }

                context.Response.StatusCode = 401;
            }
            else
            {
                await _next(context);
            }
        }
    }
}