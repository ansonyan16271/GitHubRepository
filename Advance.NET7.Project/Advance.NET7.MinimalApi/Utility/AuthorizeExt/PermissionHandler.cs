using Microsoft.AspNetCore.Authorization;

namespace Advance.NET7.MinimalApi.Utility.AuthorizeExt
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// 在这里 就可以扩展逻辑
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            HttpContext httpContext = context.Resource as HttpContext;
            var isAuthenticated = context?.User?.Identity?.IsAuthenticated;//是否解析到用户信息
            if (isAuthenticated.HasValue && isAuthenticated.Value)
            {
                context?.Succeed(requirement);//验证成功了
            }
            else
            {
                context?.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
