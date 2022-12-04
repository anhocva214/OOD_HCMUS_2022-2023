namespace financial_management_service.Core.Dtos
{
    public class WalletDto
    {
    }

    public class WalletReqDto
    {
        public string? UserId { get; set; }
        public string? WalletName { get; set; }
        public decimal? WalletBalance { get; set; }
        public WalletReqDto()
        {
            UserId = String.Empty;
            WalletName = String.Empty;
            WalletBalance = 0;
        }
    }

    public class UpdateWalletReqDto
    {
        public string? WalletId { get; set; }
        public string? WalletName { get; set; }
        public decimal? WalletBalance { get; set; }
        public UpdateWalletReqDto()
        {
            WalletId = String.Empty;
            WalletName = String.Empty;
            WalletBalance = 0;
        }
    }
}
