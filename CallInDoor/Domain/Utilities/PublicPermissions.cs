using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Utilities
{
    public static class PublicPermissions
    {
        public static string Permission = "Permissions";

        public static class User
        {                                      
            public const string GetAllUsersList = "user.GetAllUsersList";
            public const string EditUser = "user.Edit";

        }
    }
}