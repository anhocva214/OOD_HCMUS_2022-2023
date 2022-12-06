using System;
namespace financial_management_service.Core.Object
{
	public class SqlParameter
	{
		public SqlParameter()
		{
			Key = String.Empty;
			Value = String.Empty;
		}

		public SqlParameter(string key,string value)
		{
			this.Key = key;
			this.Value = value;
		}

		public string Key { get; set; }
		public string Value { get; set; }
	}
}

