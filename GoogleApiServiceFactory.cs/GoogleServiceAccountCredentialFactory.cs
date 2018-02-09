namespace GoogleApiServiceFactory
{
    internal static class GoogleServiceAccountCredentialFactory
    {
        public static IGoogleServiceAccount GetGoogleServiceAccountCredential(CredentialTypeEnum credentialType)
        {
            switch (credentialType)
            {
                case CredentialTypeEnum.JSON:
                    return new GoogleJSONServiceAccountCredential();
                case CredentialTypeEnum.P12:
                    return new GoogleP12ServiceAccountCredential();
                default:
                    return new GoogleNullServiceAccountCredential();
            }
        }
    }
}
