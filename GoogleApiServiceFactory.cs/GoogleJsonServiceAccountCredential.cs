using Google.Apis.Auth.OAuth2;
using Google.Apis.Json;
using System.IO;

namespace GoogleApiServiceFactory
{
    internal class GoogleJSONServiceAccountCredential : IGoogleServiceAccount
    {
        public ServiceAccountCredential BuildCredential(GoogleServiceParameter parameters)
        {
            ServiceAccountCredential credential;
            var credentialParameters = NewtonsoftJsonSerializer.Instance.Deserialize<JsonCredentialParameters>(File.ReadAllText(parameters.AccountFilePath));
            using (var stream = new FileStream(parameters.AccountFilePath, FileMode.Open, FileAccess.Read))
            {
                credential = ServiceAccountCredential.FromServiceAccountData(stream);
            }
            var credentialforuser = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(parameters.AccountEmail)
            {
                Scopes = parameters.ApiScopes,
                User = string.IsNullOrEmpty(parameters.UserToImpersonate) ? parameters.AccountAdminUser : parameters.UserToImpersonate,
                Key = credential.Key
            }.FromPrivateKey(credentialParameters.PrivateKey));
            return credentialforuser;
        }
    }
}
