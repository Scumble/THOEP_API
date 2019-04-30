using System;
using System.Collections.Generic;
using System.Text;

namespace THOEP.Services.Helpers
{
    public static class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
                public const string Admin = "adm", Password = "password";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
                public const string AdminAccess = "admin_access";
            }
        }
    }
}
