using Google.Apis.Auth.OAuth2;

namespace GoogleApiServiceFactory
{
    internal class GoogleNullServiceAccountCredential : IGoogleServiceAccount
    {
        public ServiceAccountCredential BuildCredential(GoogleServiceParameter parameters)
        {
            return new ServiceAccountCredential(null);
        }
    }
}
