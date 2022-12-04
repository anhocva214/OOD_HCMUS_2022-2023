using System.ComponentModel;

namespace financial_management_service.Extensions
{
	public enum PermissionCodeEn
	{
		[Description("is_permission_post_check")] IsPermissionPostCheck, //POSTCHECK
		[Description("is_permission_receive_email")] IsPermissionReceiveEmail, //POSTCHECK EMAIL

		[Description("is_permission_op1")] IsPermissionOp1, //HKV OP1
		[Description("is_permission_op2")] IsPermissionOp2, //HKV OP2

		[Description("is_permission_input_document")] IsPermissionInputDocument, //CAVET nhập giấy tờ
		[Description("is_permission_check_document")] IsPermissionCheckDocument, //CAVET KIỂM TRA GIẤY TỜ
		[Description("is_permission_receive_document")] IsPermissionReceiveDocument, //CAVET NHẬN GIẤY TỜ
		[Description("is_permission_assign_document")] IsPermissionAssignDocument, //CAVET PHÂN BỔ GIẤY TỜ
		[Description("is_permission_document_storage")] IsPermissionDocumentSrorage, //CAVET LƯU KHO GIẤY TỜ
		[Description("is_permission_payout_document")] IsPermissionPayoutDocument, //CAVET xuất trả giấy tờ
		[Description("is_permission_print_tyl")] IsPermissionPrintTYL, //CAVET in thư cảm ơn

		[Description("is_permission_reconciliation")] IsPermissionReconciliation //Quyền đối soát
	}
}

