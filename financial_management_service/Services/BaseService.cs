using financial_management_service.Core.Object;

namespace financial_management_service.Services
{
    public class BaseService
	{
		protected CurrentUserObj CurrentUser { get; set; }

        public BaseService() => CurrentUser = new CurrentUserObj();
    }
}

