using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using VisitorManagementSystems.Constants;

namespace VisitorManagementSystems.Extentions
{
    public static class IdentityExtention
    {
        public static int GetUserId(this IIdentity identity)
        {
            return Convert.ToInt32(((ClaimsIdentity)identity).Claims.ToList().Find(c => c.Type == UserAuthentication.CLAIM_UserId) == null ?
                    "" : ((ClaimsIdentity)identity).Claims.ToList().Find(c => c.Type == UserAuthentication.CLAIM_UserId).Value);
        }
        public static int GetRoleId(this IIdentity identity)
        {
            return Convert.ToInt32(((ClaimsIdentity)identity).Claims.ToList().Find(c => c.Type == UserAuthentication.CLAIM_UserRoleId) == null ?
                    "" : ((ClaimsIdentity)identity).Claims.ToList().Find(c => c.Type == UserAuthentication.CLAIM_UserRoleId).Value);
        }
        public static string GetUserRole(this IIdentity identity)
        {
            return ((ClaimsIdentity)identity).Claims.ToList().Find(c => c.Type == UserAuthentication.CLAIM_UserRoleName) == null ?
                    "" : ((ClaimsIdentity)identity).Claims.ToList().Find(c => c.Type == UserAuthentication.CLAIM_UserRoleName).Value;
        }
    }
}
