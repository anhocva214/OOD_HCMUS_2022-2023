using financial_management_service.Core.Exceptions;
using financial_management_service.Utils;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace financial_management_service.Core.Attributes
{
    public class CorsPolicyEnableAttribute : ActionFilterAttribute
    {
        protected string[] PermissionCodes;

        public CorsPolicyEnableAttribute() => PermissionCodes = Array.Empty<string>();

        public override void OnActionExecuting(ActionExecutingContext context) => Check(context);

        private static void Check(ActionExecutingContext context)
        {
            var origin = context.HttpContext.Request.Headers.Origin.ToString();

            var corsPolicy = ConfigManager.EnableCors;

            if (corsPolicy == null || !corsPolicy.Any()) return;

            //if (origin.IsNullOrEmpty() || !corsPolicy.Any(x => x.ToLower().Equals(origin.ToLower()))) throw new CorsException(origin);
        }
    }
}

