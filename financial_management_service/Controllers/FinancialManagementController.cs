using financial_management_service.Core.Attributes;
using financial_management_service.Core.Dtos;
using financial_management_service.Core.Entities;
using financial_management_service.Extensions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace financial_management_service.Controllers;

[ApiController]
[Route("")]
[CorsPolicyEnable]
[EnableCors("AllowOrigin")]
public class FinancialManagementController : ApiControllerBase
{
    private readonly IServiceProvider _sp;

    public FinancialManagementController(IHttpContextAccessor contextAccessor, IServiceProvider sp)
    {
        this._sp = sp;
        base.Init(contextAccessor);
    }

    [SwaggerOperation("Đăng ký tài khoản")]
    [HttpPost]
    [Route("register-user")]
    public async Task RegisterUser(UserReqDto dto) => await _sp.InitIRegisterUserService().Execute(dto);

    [SwaggerOperation("Đăng nhập")]
    [HttpPost]
    [Route("login")]
    public async Task<LoginResDto> Login(LoginReqDto dto) => await _sp.InitILoginService().Execute(dto);

    [SwaggerOperation("Quên mật khẩu")]
    [HttpPut]
    [Route("forgot-password")]
    public async Task ForgotPassword(ForgotPasswordReqDto dto) => await _sp.InitIForgotPasswordService().Execute(dto);

    [SwaggerOperation("Thêm ví")]
    [HttpPost]
    [Route("add-wallet")]
    public async Task<Wallet> AddWallet(WalletReqDto dto) => await _sp.InitIAddWalletService().Execute(dto);

    [SwaggerOperation("Cập nhật thông tin ví")]
    [HttpPut]
    [Route("update-wallet")]
    public async Task<Wallet> UpdateWallet(UpdateWalletReqDto dto) => await _sp.InitIUpdateWalletService().Execute(dto);

    [SwaggerOperation("Xoá ví")]
    [HttpDelete]
    [Route("delete-wallet/{walletId}")]
    public async Task DeleteWallet(string walletId) => await _sp.InitIDeleteWalletService().Execute(walletId);

    [SwaggerOperation("Lấy danh sách ví theo tài khoản đăng nhập")]
    [HttpGet]
    [Route("get-wallets/{userId}")]
    public async Task<List<Wallet>> GetWallets(string userId) => await _sp.InitIGetWalletsService().Execute(userId);

    [SwaggerOperation("Lấy danh sách danh mục")]
    [HttpGet]
    [Route("get-categories")]
    public async Task<List<Categories>> GetCategories() => await _sp.InitIGetCategoriesService().Execute();

    [SwaggerOperation("Thêm giao dịch")]
    [HttpPost]
    [Route("add-transaction")]
    public async Task<Transaction> AddTransaction(TransactionReqDto dto) => await _sp.InitIAddTransactionService().Execute(dto);

    [SwaggerOperation("Sửa giao dịch")]
    [HttpPut]
    [Route("update-transaction")]
    public async Task<Transaction> UpdateTransaction(UpdateTransactionReqDto dto) => await _sp.InitIUpdateTransactionService().Execute(dto);

    [SwaggerOperation("Xoá giao dịch")]
    [HttpDelete]
    [Route("delete-transaction/{transactionId}")]
    public async Task DeleteTransaction(string transactionId) => await _sp.InitIDeleteTransactionService().Execute(transactionId);

    [SwaggerOperation("Lấy danh sách giao dịch")]
    [HttpGet]
    [Route("get-transactions")]
    public async Task<List<SearchTransactionResDto>> GetTransactions(SearchTransactionReqDto dto) => await _sp.InitIGetTransactionsService().Execute(dto);

    [SwaggerOperation("Cập nhật thông tin tài khoản")]
    [HttpPut]
    [Route("update-user")]
    public async Task<LoginResDto> UpdateUser(UpdateUserReqDto dto) => await _sp.InitIUpdateUserService().Execute(dto);

    [SwaggerOperation("Lấy thông tin tài khoản")]
    [HttpGet]
    [Route("get-user/{userId}")]
    public async Task<Users> GetUserInfo(string userId) => await _sp.InitIGetUserService().Execute(userId);

    [SwaggerOperation("Thêm danh mục")]
    [HttpPost]
    [Route("add-categories")]
    public async Task<Categories> AddCategory(AddCategoryReqDto dto) => await _sp.InitIAddCategoryService().Execute(dto);

    [SwaggerOperation("Sửa danh mục")]
    [HttpPut]
    [Route("update-categories")]
    public async Task<string> UpdateCategory(UpdateCategoryReqDto dto) => await _sp.InitIUpdateCategoryService().Execute(dto);

    [SwaggerOperation("Xoá danh mục")]
    [HttpDelete]
    [Route("delete-categories/{categoryId}")]
    public async Task DeleteCategory(string categoryId) => await _sp.InitIDeleteCategoryService().Execute(categoryId);
}


		
		
		