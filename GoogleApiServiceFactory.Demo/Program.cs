using Google.Apis.Admin.Directory.directory_v1;
using System;

namespace GoogleApiServiceFactory.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConnectDirectoryService();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception ocurred:");
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Press space to exit");
            Console.ReadKey();
        }

        static void ConnectDirectoryService()
        {
            Console.WriteLine("*********************BEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEPP***********************");
            Console.WriteLine("******************************Google Service Bot*********************************");

            var gApiServiceFactory = new GoogleServiceFactory();

            gApiServiceFactory.Parameters = new GoogleServiceParameter
            {
                AccountAdminUser = "service.admin@domain.xx",
                AccountEmail = "account-emil@g-proj.iam.gserviceaccount.com",
                ApplicationName = "G-App Services",
                AccountFilePath = "json_path",
                ApiScopes = new string[] { DirectoryService.Scope.AdminDirectoryUser },
                CredentialType = CredentialTypeEnum.JSON
            };

            Console.WriteLine("User name to search: ");
            var userName = Console.ReadLine();

            Console.Write(Environment.NewLine);

            Console.WriteLine("JSON");

            var directoryService = gApiServiceFactory.CreateServiceObject<DirectoryService>();
            var usersListRequest = directoryService.Users.Get(userName);
            var usersListResult = usersListRequest.Execute();

            Console.Write(Environment.NewLine);

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(usersListResult));

            Console.Write(Environment.NewLine);


            Console.WriteLine("P12");

            Console.Write(Environment.NewLine);

            gApiServiceFactory.Parameters.CredentialType = CredentialTypeEnum.P12;
            gApiServiceFactory.Parameters.AccountFilePath = "p12_path";

            directoryService = gApiServiceFactory.CreateServiceObject<DirectoryService>();
            usersListRequest = directoryService.Users.Get(userName);
            usersListResult = usersListRequest.Execute();

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(usersListResult));

            Console.ReadKey();
        }
    }
}
