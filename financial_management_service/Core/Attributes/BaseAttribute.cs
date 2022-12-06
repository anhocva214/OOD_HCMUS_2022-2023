using financial_management_service.Core.Exceptions;
using financial_management_service.Core.Object;
using financial_management_service.Extensions;
using financial_management_service.Utils;
using Microsoft.AspNetCore.Mvc.Filters;

namespace financial_management_service.Core.Attributes
{
    public class BaseAttribute : ActionFilterAttribute
    {
        protected string[] PermissionCodes;

        protected BaseAttribute()  => PermissionCodes = Array.Empty<string>();

        protected  void CheckPermission(ActionExecutingContext context)
        {
            try
            {
                var val = context.HttpContext.Items["X-Current-User"]?.ToString();
                If.IsTrue(val == null).ThrPermission();

                var currentUser = val.ToObject<CurrentUserObj>();
                If.IsTrue(currentUser == null || currentUser.Sid.IsNullOrEmpty()).ThrPermission();

                /*Kiểm tra nếu tài khoản có quyền thì đều được truy cập*/
                If.IsTrue(!HasPermission(currentUser)).ThrPermission();

            }
            catch (Exception ex)
            {
                Console.WriteLine("BaseAttribute.CheckPermission: " + ex.Message);
                Console.WriteLine("BaseAttribute.CheckPermission:" + ex);

                throw new PermissionException();
            }
        }

        private bool HasPermission(CurrentUserObj? currentUser)
        {
            if (PermissionCodes == null) return true;

            foreach (var item in PermissionCodes)
                if (currentUser.HasPermission(item))
                    return true;

            return false;
        }
    }
}

