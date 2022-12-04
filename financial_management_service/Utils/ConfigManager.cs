using financial_management_service.Extensions;

namespace financial_management_service.Utils
{
	public class ConfigManager
	{
        private static readonly Dictionary<string, string> Dic = new Dictionary<string, string>();

        public static string SN_Host => Dic["SN_Host"];
        public static string SN_Role_API => Dic["SN_Role_API"];
        public static string SN_MyStaffs_API => Dic["SN_MyStaffs_API"];
        public static List<string> EnableCors
        {
            get
            {
                var policy = Dic["EnableCors"];
                if (policy.IsNullOrEmpty()) return new List<string>();

                List<string> ret = policy.Split(',').ToList();
                return ret;
            }
        }

        public void Init(ConfigurationManager cf)
        {
            InitSNHost(cf);
            InitEnableCors(cf);
        }

        private static void InitSNHost(ConfigurationManager cf)
        {
            var val = cf["SaleNetwork:Host"];
            if (val.EndsWith("/"))
                val = val[0..^1];

            Dic.Add("SN_Host", val);
        }
        private static void InitEnableCors(ConfigurationManager cf) => Dic.Add("EnableCors", cf["EnableCors"]);
    }
}

