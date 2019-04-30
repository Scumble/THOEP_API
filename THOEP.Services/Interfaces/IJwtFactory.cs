using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace THOEP.Services.Interfaces
{

    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity);
        Task<string> GenerateEncodedTokenAdmin(string userName, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
        ClaimsIdentity GenerateClaimsIdentityAdmin(string userName, string id);

    }
}
