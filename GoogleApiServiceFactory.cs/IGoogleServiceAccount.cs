using Google.Apis.Auth.OAuth2;

namespace GoogleApiServiceFactory
{
    internal interface IGoogleServiceAccount
    {
        ServiceAccountCredential BuildCredential(GoogleServiceParameter parameters);
    }
}
