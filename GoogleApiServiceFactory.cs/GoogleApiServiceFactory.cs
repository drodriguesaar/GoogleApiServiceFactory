using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System;

namespace GoogleApiServiceFactory
{

    public class GoogleServiceFactory
    {
        public GoogleServiceParameter Parameters { get; set; }

        /// <summary>
        /// Creates a new instance of a google api service class using service authentication.
        /// </summary>
        /// <typeparam name="T">Type T of a google api service class, ex: GmailService, DirectoryService e etc.</typeparam>
        /// <returns>The authenticated google api service class instance.</returns>
        public T CreateServiceObject<T>()
        {
            var credential = CreateServiceAccountCredential();

            var baseClientService = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Parameters.ApplicationName,
            };
            return (T)Activator.CreateInstance(typeof(T), new object[] { baseClientService });
        }
        private ServiceAccountCredential CreateServiceAccountCredential()
        {
            var serviceAccountCredential = GoogleServiceAccountCredentialFactory.GetGoogleServiceAccountCredential(this.Parameters.CredentialType);

            return serviceAccountCredential.BuildCredential(this.Parameters);
        }
    }
}
