using financial_management_service.Services;
using financial_management_service.Infrastructure.Callout;
using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Utils;
using Microsoft.EntityFrameworkCore;
using financial_management_service.Core.Object;

namespace financial_management_service
{
    public class ServiceRegister
	{
		protected ServiceRegister() { }

		public static void Run(WebApplicationBuilder builder)
        {
			var configuration = builder.Configuration;
			new ConfigManager().Init(configuration);

			builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseInMemoryDatabase("Financial_Management"));
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddMemoryCache();
			builder.Services.AddHttpClient();
			
			builder.Services.AddSingleton<IHttpClientService, HttpClientService>();
			builder.Services.AddSingleton<IMailService>(a => new MailService(new MailSettings()
			{
				Host = configuration["MailSettings:Host"],
				Mail = configuration["MailSettings:Mail"],
				Password = configuration["MailSettings:Password"],
				Port = int.Parse(configuration["MailSettings:Port"]),
				EnableSSL = bool.Parse(configuration["MailSettings:EnableSSL"]),
				MailSubFix = configuration["MailSettings:MailSubFix"]
			}));

			builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
			builder.Services.AddTransient<IRegisterUserService, RegisterUserService>();
			builder.Services.AddTransient<IGetTransactionsService, GetTransactionsService>();
			builder.Services.AddTransient<IDeleteTransactionService, DeleteTransactionService>();
			builder.Services.AddTransient<IUpdateTransactionService, UpdateTransactionService>();
			builder.Services.AddTransient<IGetCategoriesService, GetCategoriesService>();
			builder.Services.AddTransient<IGetWalletsService, GetWalletsService>();
			builder.Services.AddTransient<IDeleteWalletService, DeleteWalletService>();
			builder.Services.AddTransient<IUpdateWalletService, UpdateWalletService>();
			builder.Services.AddTransient<IAddWalletService, AddWalletService>();
			builder.Services.AddTransient<IAddTransactionService, AddTransactionService>();
			builder.Services.AddTransient<IForgotPasswordService, ForgotPasswordService>();
			builder.Services.AddTransient<ILoginService, LoginService>();
			builder.Services.AddTransient<IUpdateUserService, UpdateUserService>();
            builder.Services.AddTransient<IAddCategoryService, AddCategoryService>();
            builder.Services.AddTransient<IUpdateCategoryService, UpdateCategoryService>();
            builder.Services.AddTransient<IDeleteCategoryService, DeleteCategoryService>();
            builder.Services.AddTransient<IGetUserService, GetUserService>();
        }
    }
}

