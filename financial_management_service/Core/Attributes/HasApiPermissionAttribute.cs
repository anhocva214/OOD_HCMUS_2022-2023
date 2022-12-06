using Microsoft.AspNetCore.Mvc.Filters;

namespace financial_management_service.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class HasApiPermissionAttribute : BaseAttribute
    {
        public HasApiPermissionAttribute(params string[] permissionCodes) => this.PermissionCodes = permissionCodes;

        public override  void OnActionExecuting(ActionExecutingContext context) =>  base.CheckPermission(context);
    }
}

