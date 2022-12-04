using System;
using financial_management_service.Core.Exceptions;
using financial_management_service.Extensions;

namespace financial_management_service.Utils
{
	public class If
	{
        private enum IfType
        {
            IS_NULL,
            IS_TRUE,
            NONE
        }

        private IfType Type { get; set; }
		private string IsNullVal { get; set; }
        private bool IsTrueVal { get; set; }

        public If()
		{
			Type = IfType.NONE;
            IsNullVal = string.Empty;
        }

        private If(IfType input,string value)
        {
			Type = input;
			IsNullVal = value;
        }

        private If(IfType input, bool value)
        {
            IsNullVal = String.Empty;
            Type = input;
            IsTrueVal = value;
        }

        public static If IsNull(string input) => new If(IfType.IS_NULL, input);

        public static If IsTrue(bool input) => new If(IfType.IS_TRUE, input);

        public void ThrBiz(string code,string message)
        {
			if (Type == IfType.IS_NULL && this.IsNullVal.IsNullOrEmpty()) throw new BizException(code, message);

            if(Type == IfType.IS_TRUE && IsTrueVal) throw new BizException(code, message);
        }

        public void ThrUnhandle()
        {
            if (Type == IfType.IS_NULL && this.IsNullVal.IsNullOrEmpty()) throw new UnhandledException();

            if (Type == IfType.IS_TRUE && IsTrueVal) throw new UnhandledException();
        }

        public void ThrPermission()
        {
            if (Type == IfType.IS_NULL && this.IsNullVal.IsNullOrEmpty()) throw new PermissionException();

            if (Type == IfType.IS_TRUE && IsTrueVal) throw new PermissionException();
        }

        public void ThrUnauthorize()
        {
            if (Type == IfType.IS_NULL && this.IsNullVal.IsNullOrEmpty()) throw new UnauthorizedException();

            if (Type == IfType.IS_TRUE && IsTrueVal) throw new UnauthorizedException();
        }
    }
}

