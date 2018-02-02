using Google.Apis.Admin.Directory.directory_v1;

namespace GoogleApiServiceFactory.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectDirectoryService();
        }

        static void ConnectDirectoryService() {
            var gApiServiceFactory = new GoogleApiServiceAccountFactory
            {
                GoogleServiceAccountAdminUser = "service.admin@domain.br",
                GoogleServiceAccountEmail = "serviceuser@g-proj.iam.gserviceaccount.xx",
                GoogleServiceAccountFilePath = @"X:\\cert.json",
                GoogleServiceApplicationName = "App G-Service",
            };

            var directoryService = gApiServiceFactory.GetService<DirectoryService>(new string[] { DirectoryService.Scope.AdminDirectoryUser });
            var usersListRequest = directoryService.Users.Get("your.user@domain.xx");
            var usersListResult = usersListRequest.Execute();

            gApiServiceFactory.IsJSON = false;
            gApiServiceFactory.GoogleServiceAccountFilePath = @"\\cert.p12";

            var directoryServiceP12 = gApiServiceFactory.GetService<DirectoryService>(new string[] { DirectoryService.Scope.AdminDirectoryUser });
            var usersListRequestP12 = directoryService.Users.Get("your.user@domain.xx");
            var usersListResultP12 = usersListRequest.Execute();
        }
    }
}
