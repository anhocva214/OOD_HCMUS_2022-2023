using financial_management_service.Core.Object;

namespace financial_management_service.Extensions
{
	public static class CurrentUserExt
	{
        public static bool HasPermission(this CurrentUserObj? obj, string permission)
        {
            try
            {
                if (obj == null || obj.Authorization == null || obj.Authorization.permissions == null) return false;

                foreach (var item in obj.Authorization.permissions)
                    if (item.Scopes.FirstOrDefault(a => string.Equals(a, permission)) != null)
                        return true;

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("CurrentUserExt.HasPermission" + ex.Message);
                return false;
            }
        }
    }
}

