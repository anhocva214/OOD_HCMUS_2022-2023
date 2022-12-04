using financial_management_service.Extensions;

namespace financial_management_service.Utils
{
	public class ConfigManager
	{
        private static readonly Dictionary<string, string> Dic = new Dictionary<string, string>();

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
            InitEnableCors(cf);
        }

        private static void InitEnableCors(ConfigurationManager cf) => Dic.Add("EnableCors", cf["EnableCors"]);
    }
}

