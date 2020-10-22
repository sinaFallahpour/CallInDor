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
            public const string GetAll = "USer.GEtAll";
            public const string userEdit = "user.Edit";
        }
    }
}