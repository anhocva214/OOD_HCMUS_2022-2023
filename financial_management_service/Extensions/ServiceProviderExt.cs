using financial_management_service.Services;

namespace financial_management_service.Extensions
{
    public static class ServiceProviderExt
    {
		public static ILoginService InitILoginService(this IServiceProvider sp) => sp.GetService<ILoginService>() ?? throw new Exception();
		public static IForgotPasswordService InitIForgotPasswordService(this IServiceProvider sp) => sp.GetService<IForgotPasswordService>() ?? throw new Exception();
        public static IAddWalletService InitIAddWalletService(this IServiceProvider sp) => sp.GetService<IAddWalletService>() ?? throw new Exception();
        public static IUpdateWalletService InitIUpdateWalletService(this IServiceProvider sp) => sp.GetService<IUpdateWalletService>() ?? throw new Exception();
        public static IDeleteWalletService InitIDeleteWalletService(this IServiceProvider sp) => sp.GetService<IDeleteWalletService>() ?? throw new Exception();
        public static IGetWalletsService InitIGetWalletsService(this IServiceProvider sp) => sp.GetService<IGetWalletsService>() ?? throw new Exception();
        public static IGetCategoriesService InitIGetCategoriesService(this IServiceProvider sp) => sp.GetService<IGetCategoriesService>() ?? throw new Exception();
        public static IAddTransactionService InitIAddTransactionService(this IServiceProvider sp) => sp.GetService<IAddTransactionService>() ?? throw new Exception();
		public static IUpdateTransactionService InitIUpdateTransactionService(this IServiceProvider sp) => sp.GetService<IUpdateTransactionService>() ?? throw new Exception();		
        public static IDeleteTransactionService InitIDeleteTransactionService(this IServiceProvider sp) => sp.GetService<IDeleteTransactionService>() ?? throw new Exception();		
        public static IGetTransactionsService InitIGetTransactionsService(this IServiceProvider sp) => sp.GetService<IGetTransactionsService>() ?? throw new Exception();
		public static IRegisterUserService InitIRegisterUserService(this IServiceProvider sp) => sp.GetService<IRegisterUserService>() ?? throw new Exception();
    }
}




