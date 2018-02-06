using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;

namespace GoogleApiServiceFactory
{
    public class GoogleNullServiceAccountCredential : IGoogleServiceAccount
    {
        public ServiceAccountCredential BuildCredential(GoogleServiceParameter parameters)
        {
            return new ServiceAccountCredential(null);
        }
    }
}
