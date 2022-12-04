using financial_management_service.Core.Object;
using financial_management_service.Core.Exceptions;
using financial_management_service.Security;
using Microsoft.AspNetCore.Mvc;
using financial_management_service.Utils;
using financial_management_service.Extensions;

namespace financial_management_service.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected CurrentUserObj _currentUser { get; set; }

        public ApiControllerBase() => _currentUser = new CurrentUserObj();

        protected void Init(IHttpContextAccessor contextAccessor)
        {
            //try
            //{
            //    If.IsTrue(contextAccessor == null || contextAccessor.HttpContext == null).ThrUnauthorize();

            //    var array = contextAccessor?.HttpContext.Request.Headers["Authorization"].ToString().Split('.').ToArray();
            //    If.IsTrue(array == null || array.Length <= 1).ThrUnauthorize();
                
            //    var val = Base64Security.Decode(array[1]!);
            //    If.IsTrue(val.IsNullOrEmpty()).ThrUnauthorize();

            //    this._currentUser = val.ToObject<CurrentUserObj>();
            //    If.IsTrue(_currentUser == null || _currentUser.Sid.IsNullOrEmpty()).ThrUnauthorize();

            //    contextAccessor.HttpContext.Items["X-Current-User"] = val;
            //}
            //catch
            //{
            //    throw new UnauthorizedException();
            //}
        }
    }
}