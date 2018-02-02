using Google.Apis.Auth.OAuth2;
using Google.Apis.Json;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace GoogleApiServiceFactory
{
    /// <summary>
    /// Used to create authenticated google service accounts, both with .json or .p12 certificates.
    /// </summary>
    public class GoogleApiServiceAccountFactory
    {
        public string GoogleServiceApplicationName { get; set; }
        public string GoogleServiceAccountFilePath { get; set; }
        public string GoogleServiceAccountAdminUser { get; set; }
        public string GoogleServiceAccountEmail { get; set; }

        /// <summary>
        /// Property used to define if the authentication will be made using a .json file or .p12 file.
        /// </summary>
        public bool IsJSON { get; set; }

        public GoogleApiServiceAccountFactory()
        {
            IsJSON = true;
        }

        /// <summary>
        /// Generate a new instance of a authenticated google service account.
        /// </summary>
        /// <typeparam name="T">Instance of a Google Service, ex: GmailService, DirectoryService e etc.</typeparam>
        /// <param name="scopes">Scope list</param>
        /// <returns>The authenticated google service account instance.</returns>
        public T GetService<T>(IEnumerable<string> scopes)
        {
            var credential = GetServiceAccountCredential(scopes);

            var baseClientService = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = GoogleServiceApplicationName,
            };
            return (T)Activator.CreateInstance(typeof(T), new object[] { baseClientService });
        }
        /// <summary>
        /// Generate a new instance of a authenticated google service account impersonating a user.
        /// </summary>
        /// <typeparam name="T">Instance of a Google Service, ex: GmailService, DirectoryService e etc.</typeparam>
        /// <param name="scopes">Scope list</param>
        /// <param name="userToImpersonate">User to impersonate to access resources like Calendar, Gmail e etc.</param>
        /// <returns>The impersonated and authenticated google service account instance.</returns>
        public T GetService<T>(IEnumerable<string> scopes, string userToImpersonate)
        {
            var credential = GetServiceAccountCredential(scopes, userToImpersonate: userToImpersonate);
            var baseClientService = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = GoogleServiceApplicationName,
            };
            return (T)Activator.CreateInstance(typeof(T), new object[] { baseClientService });
        }

        private ServiceAccountCredential GetServiceAccountCredential(IEnumerable<string> scopes, string userToImpersonate = "")
        {
            return IsJSON ? GetFromJSON(scopes, userToImpersonate) : GetFromP12(scopes, userToImpersonate);
        }

        private ServiceAccountCredential GetFromJSON(IEnumerable<string> scopes, string userToImpersonate = "")
        {
            ServiceAccountCredential credential;
            var credentialParameters = NewtonsoftJsonSerializer.Instance.Deserialize<JsonCredentialParameters>(File.ReadAllText(GoogleServiceAccountFilePath));
            using (var stream = new FileStream(GoogleServiceAccountFilePath, FileMode.Open, FileAccess.Read))
            {
                credential = ServiceAccountCredential.FromServiceAccountData(stream);
            }
            var credentialforuser = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(GoogleServiceAccountEmail)
            {
                Scopes = scopes,
                User = string.IsNullOrEmpty(userToImpersonate) ? GoogleServiceAccountAdminUser : userToImpersonate,
                Key = credential.Key
            }.FromPrivateKey(credentialParameters.PrivateKey));
            return credentialforuser;
        }

        private ServiceAccountCredential GetFromP12(IEnumerable<string> scopes, string userToImpersonate = "")
        {
            var credential = new X509Certificate2(GoogleServiceAccountFilePath, "notasecret", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);

            var serviceAccountCredential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(GoogleServiceAccountEmail)
            {
                Scopes = scopes,
                User = string.IsNullOrEmpty(userToImpersonate) ? GoogleServiceAccountAdminUser : userToImpersonate
            }.FromCertificate(credential));
            return serviceAccountCredential;
        }
    }
}
