using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UI
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomAuthorizeAttribute:Attribute, IAuthorizationFilter
    {
        private readonly List<string> _roles = new();

        public CustomAuthorizeAttribute()
        {
            _roles = null;
        }

        public CustomAuthorizeAttribute(string role)
        {
            _roles.Add(role);
        }

        public CustomAuthorizeAttribute(string[] roles)
        {
            _roles.AddRange(roles);
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.HttpContext.Features.Get<IEndpointFeature>().Endpoint?.Metadata.GetMetadata<CustomAuthorizeAttribute>() != null;
            if (allowAnonymous)
                return;

            try
            {

                var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);
                var tokenS = jsonToken as JwtSecurityToken;

                if(_roles == null)
                    return;


                var role = tokenS.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role).Value;

                if (!_roles.Any(r => string.Equals(r, role)))
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            catch (Exception e)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
