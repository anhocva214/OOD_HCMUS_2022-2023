﻿using financial_management_service.Core.Constant;
using financial_management_service.Core.Dtos;
using financial_management_service.Core.Entities;
using financial_management_service.Extensions;
using financial_management_service.Infrastructure.DBContext;
using financial_management_service.Utils;

namespace financial_management_service.Services
{
	public interface IAddTransactionService
	{
		Task<Transaction> Execute(TransactionReqDto dto);
	}

	public class AddTransactionService : BaseService, IAddTransactionService
	{
		private readonly IUnitOfWork _uok;

		public AddTransactionService(IUnitOfWork uok) => _uok = uok;

		public async Task<Transaction> Execute(TransactionReqDto dto)
		{
			await Validate(dto);

			var transaction = InitTransaction(dto);

			_uok.Transaction.Add(transaction);

			await _uok.CompleteAsync();

			return transaction;
		}

		private async Task Validate(TransactionReqDto dto)
		{
			If.IsTrue(dto.UserId.IsNullOrEmpty() || !dto.UserId.IsGuid()).ThrBiz(ErrorCode._400_01, "Dữ liệu truyền vào không đúng.");

			If.IsTrue(dto.CategoryId.IsNullOrEmpty() || !dto.CategoryId.IsGuid()).ThrBiz(ErrorCode._400_01, "Dữ liệu truyền vào không đúng.");

			If.IsTrue(!dto.Amount.HasValue).ThrBiz(ErrorCode._400_02, "Vui lòng điền số tiền.");

			If.IsTrue(dto.Date != null).ThrBiz(ErrorCode._400_03, "Vui lòng chọn ngày.");

			If.IsTrue(await _uok.Users.GetByIdAsync(dto.UserId) == null).ThrBiz(ErrorCode._400_04, "Không tìm thấy dữ liệu tài khoản của bạn.");

			If.IsTrue(await _uok.Categories.GetByIdAsync(dto.CategoryId) == null).ThrBiz(ErrorCode._400_06, "Không tìm thấy dữ liệu danh mục.");
		}

		private static Transaction InitTransaction(TransactionReqDto dto)
        {
			return new Transaction()
			{
				UserId = dto.UserId,
				CategoryId = dto.CategoryId,
				Amount = dto.Amount,
				Date = dto.Date,
				Note = dto.Note,
				CreatedAt = DateTime.Now,
				ModifiedAt = DateTime.Now
			};
		}
	}
}