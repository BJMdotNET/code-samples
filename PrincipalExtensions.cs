using Samples.Shared.Exceptions;
using Samples.Shared.Languages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;

namespace Samples.Shared.Security
{
    public static class PrincipalExtensions
    {
        public static List<string> GetSamplesRoles(this IPrincipal principal)
        {
            try
            {
                return principal.GetClaims()
                    .Where(claim => claim.Type == ClaimTypes.GroupSid)
                    .Select(claim =>
                        new
                        {
                            claim,
                            roleName = new SecurityIdentifier(claim.Value).Translate(typeof(NTAccount)).Value
                        })
                    .Where(x => x.roleName.StartsWith(ActiveDirectorySettings.DomainAndPrefix,
                        StringComparison.InvariantCultureIgnoreCase))
                    .Select(x => x.roleName)
                    .ToList();
            }
            catch (Exception exc)
            {
                var principalName = principal.GetName();
                var methodName = MethodBase.GetCurrentMethod().Name;

                if (principal == null)
                {
                    Trace.TraceError($"{principalName}: {methodName}: principal is NULL");
                }
                else
                {
                    var claims = principal.GetClaims();
                    if (claims == null)
                    {
                        Trace.TraceError($"{principalName}: {methodName}: claims is NULL");
                    }
                }

                LogException(principalName, methodName, exc);
                return null;
            }
        }

        public static string GetUpn(this IPrincipal principal)
        {
            try
            {
                return principal.GetSingleOrDefaultClaimValue(ClaimTypes.Upn);
            }
            catch (Exception exc)
            {
                var principalName = principal.GetName();
                var methodName = MethodBase.GetCurrentMethod().Name;

                if (principal == null)
                {
                    Trace.TraceError($"{principalName}: {methodName}: principal is NULL");
                }

                LogException(principalName, methodName, exc);
                return null;
            }
        }

        public static string GetDomain(this IPrincipal principal)
        {
            try
            {
                return principal.GetSingleOrDefaultClaimValue(CustomClaimTypes.Domain);
            }
            catch (Exception exc)
            {
                var principalName = principal.GetName();
                var methodName = MethodBase.GetCurrentMethod().Name;

                if (principal == null)
                {
                    Trace.TraceError($"{principalName}: {methodName}: principal is NULL");
                }

                LogException(principalName, methodName, exc);
                return null;
            }
        }

        private static string GetSingleOrDefaultClaimValue(this IPrincipal principal, string claimName)
        {
            try
            {
                return principal.GetClaims()
                    .SingleOrDefault(claim => claim.Type == claimName)?.Value;
            }
            catch (Exception exc)
            {
                var principalName = principal.GetName();
                var methodName = MethodBase.GetCurrentMethod().Name;

                if (principal == null)
                {
                    Trace.TraceError($"{principalName}: {methodName}: principal is NULL");
                }
                else
                {
                    var claims = principal.GetClaims();

                    if (claims == null)
                    {
                        Trace.TraceError($"{principalName}: {methodName}: claims is NULL");
                    }
                }

                LogException(principalName, methodName, exc);
                return null;
            }
        }

        private static IEnumerable<Claim> GetClaims(this IPrincipal principal)
        {
            try
            {
                return principal.GetClaimsIdentity().Claims;
            }
            catch (Exception exc)
            {
                var principalName = principal.GetName();
                var methodName = MethodBase.GetCurrentMethod().Name;

                if (principal == null)
                {
                    Trace.TraceError($"{principalName}: {methodName}: principal is NULL");
                }
                else
                {
                    var claimsIdentity = principal.GetClaimsIdentity();
                    if (claimsIdentity == null)
                    {
                        Trace.TraceError($"{principalName}: {methodName}: claimsIdentity is NULL");
                    }
                }

                LogException(principalName, methodName, exc);
                return null;
            }
        }

        public static ClaimsIdentity GetClaimsIdentity(this IPrincipal principal)
        {
            try
            {
                return (ClaimsIdentity)principal.GetClaimsPrincipal().Identity;
            }
            catch (Exception exc)
            {
                var principalName = principal.GetName();
                var methodName = MethodBase.GetCurrentMethod().Name;

                if (principal == null)
                {
                    Trace.TraceError($"{principalName}: {methodName}: principal is NULL");
                }
                else
                {
                    var claimsPrincipal = principal.GetClaimsPrincipal();
                    if (claimsPrincipal == null)
                    {
                        Trace.TraceError($"{principalName}: {methodName}: claimsPrincipal is NULL");
                    }
                }

                LogException(principalName, methodName, exc);
                return null;
            }
        }

        public static ClaimsPrincipal GetClaimsPrincipal(this IPrincipal principal)
        {
            try
            {
                return (ClaimsPrincipal)principal;
            }
            catch (Exception exc)
            {
                var principalName = principal.GetName();
                var methodName = MethodBase.GetCurrentMethod().Name;

                if (principal == null)
                {
                    Trace.TraceError($"{principalName}: {methodName}: principal is NULL");
                }

                LogException(principalName, methodName, exc);
                return null;
            }
        }

        public static string GetCulture(this IPrincipal principal)
        {
            try
            {
                return principal.FindFirstClaimValue(CustomClaimTypes.Culture)
                       ?? CultureHelper.GetDefaultCulture();
            }
            catch (Exception exc)
            {
                var principalName = principal.GetName();
                var methodName = MethodBase.GetCurrentMethod().Name;

                if (principal == null)
                {
                    Trace.TraceError($"{principalName}: {methodName}: principal is NULL");
                }

                LogException(principalName, methodName, exc);
                return null;
            }
        }

        private static string FindFirstClaimValue(this IPrincipal principal, string claim)
        {
            try
            {
                return principal.GetClaimsPrincipal().FindFirst(claim)?.Value;
            }
            catch (Exception exc)
            {
                var principalName = principal.GetName();
                var methodName = MethodBase.GetCurrentMethod().Name;

                if (principal == null)
                {
                    Trace.TraceError($"{principalName}: {methodName}: principal is NULL");
                }
                else
                {
                    var claimsPrincipal = principal.GetClaimsPrincipal();
                    if (claimsPrincipal == null)
                    {
                        Trace.TraceError($"{principalName}: {methodName}: claimsPrincipal is NULL");
                    }
                }

                LogException(principalName, methodName, exc);
                return null;
            }
        }

        private static void LogException(string principalName, string methodName, Exception exc)
        {
            var exceptionMessage = ExceptionMessageRetriever.Execute(exc);
            Trace.TraceError($"{principalName}: {methodName}: {exceptionMessage}");
        }

        private static string GetName(this IPrincipal principal)
        {
            return principal?.Identity?.Name ?? string.Empty;
        }
    }
}
