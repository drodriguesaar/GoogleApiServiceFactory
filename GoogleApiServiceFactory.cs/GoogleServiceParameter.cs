using System.Collections.Generic;

namespace GoogleApiServiceFactory
{
    public class GoogleServiceParameter
    {
        public GoogleServiceParameter()
        {
            CredentialType = CredentialTypeEnum.JSON;
        }

        public IEnumerable<string> ApiScopes { get; set; }
        public string UserToImpersonate { get; set; }
        public string ApplicationName { get; set; }
        public string AccountFilePath { get; set; }
        public string AccountAdminUser { get; set; }
        public string AccountEmail { get; set; }
        public CredentialTypeEnum CredentialType { get; set; }
    }
}
