using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Calendar.v3;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleApiFactory
{
    public class Example
    {
        void Call()
        {
            var serviceAcccount = new GoogleApiServiceAccountFactory
            {
                GoogleServiceAccountAdminUser = "services.admin@domain.xx",
                GoogleServiceAccountEmail = "user@g-proj.iam.gserviceaccount.com",
                GoogleServiceAccountFilePath = "X:\file.json",
                GoogleServiceApplicationName = "G-Api Factory"
            };

            /*Accessing proprietary domain data with service account admin user authentication.*/
            var directoryService = serviceAcccount.GetService<DirectoryService>(new string[] { DirectoryService.Scope.AdminDirectoryUser });
            var usersListRequest = directoryService.Users.List();
            var usersListResult = usersListRequest.Execute();

            /*
             DO YOUR MAGIC
             */
             
            /* Accessing data from a specific user from our domain, 
             * use the userToImpersonate overload and the service account will authenticate the user on his behalf.*/
            var calendarService = serviceAcccount.GetService<CalendarService>(new string[] { CalendarService.Scope.Calendar }, "user.me@domain.xx");
            var calendarListRequest = calendarService.CalendarList.List();
            var calendarListResult = calendarListRequest.Execute();

            /*
             DO YOUR MAGIC
             */
        }
    }
}
