using Newtonsoft.Json;

namespace financial_management_service.Core.Object
{
    public class RealmAccess
    {
        [JsonProperty("roles")]
        public List<string> Roles { get; set; }
        public RealmAccess()
        {
            Roles = new List<string>();
        }
    }

    public class Account
    {
        [JsonProperty("roles")]
        public List<string> Roles { get; set; }
        public Account()
        {
            Roles = new List<string>();
        }
    }

    public class ResourceAccess
    {
        [JsonProperty("account")]
        public Account Account { get; set; }
        public ResourceAccess()
        {
            Account = new Account();
        }
    }

    public class Permission
    {
        [JsonProperty("scopes")]
        public List<string> Scopes { get; set; }
        [JsonProperty("rsid")]
        public string Rsid { get; set; }
        [JsonProperty("rsname")]
        public string Rsname { get; set; }

        public Permission()
        {
            Scopes = new List<string>();
            Rsid = String.Empty;
            Rsname = String.Empty;
        }
    }

    public class Authorization
    {
        [JsonProperty("permissions")]
        public List<Permission> permissions { get; set; }

        public Authorization()
        {
            permissions = new List<Permission>();
        }
    }

    public class CurrentUserObj
    {
        [JsonProperty("exp")]
        public int Exp { get; set; }
        [JsonProperty("iat")]
        public int Iat { get; set; }
        [JsonProperty("jti")]
        public string Jti { get; set; }
        [JsonProperty("iss")]
        public string Iss { get; set; }
        [JsonProperty("aud")]
        public string Aud { get; set; }
        [JsonProperty("Sub")]
        public string Sub { get; set; }
        [JsonProperty("Typ")]
        public string Typ { get; set; }
        [JsonProperty("Azp")]
        public string Azp { get; set; }
        [JsonProperty("session_state")]
        public string SessionState { get; set; }
        [JsonProperty("Acr")]
        public string Acr { get; set; }
        [JsonProperty("realm_access")]
        public RealmAccess RealmAccess { get; set; }
        [JsonProperty("resource_access")]
        public ResourceAccess ResourceAccess { get; set; }
        [JsonProperty("authorization")]
        public Authorization Authorization { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
        [JsonProperty("sid")]
        public string Sid { get; set; }
        [JsonProperty("email_verified")]
        public bool EmailVerified { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("preferred_username")]
        public string PreferredUsername { get; set; }
        [JsonProperty("given_name")]
        public string GivenName { get; set; }
        [JsonProperty("family_name")]
        public string FamilyName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }

        public CurrentUserObj()
        {
            Exp = 0;
            Iat = 0;
            Jti = string.Empty;
            Iss = string.Empty;
            Aud = string.Empty;
            Sub = string.Empty;
            Typ = string.Empty;
            Azp = string.Empty;
            SessionState = string.Empty;
            Acr = string.Empty;
            RealmAccess = new RealmAccess();
            ResourceAccess = new ResourceAccess();
            Authorization = new Authorization();
            Scope = string.Empty;
            Sid = string.Empty;
            Name = string.Empty;
            PreferredUsername = string.Empty;
            GivenName = string.Empty;
            FamilyName = string.Empty;
            Email = string.Empty;
        }
    }
}

