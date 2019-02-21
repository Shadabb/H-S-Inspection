using Inspection.Models;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Azure.ActiveDirectory.GraphClient.Extensions;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Inspection.Helpers
{
    public class USerLoginDetails
    {
        private readonly string _clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private readonly string _appKey = ConfigurationManager.AppSettings["ida:ClientSecret"];
        private readonly string _aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private readonly string _graphResourceId = "https://graph.windows.net";
        private string MyAdGroup = ConfigurationManager.AppSettings["MyAdGroup"];
        private string AllowedGroupID = ConfigurationManager.AppSettings["Inspection"];

        //public async Task<string> UserDetails()
        //{
        //    var tenantId = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid")
        //        .Value;
        //    var userObjectId = ClaimsPrincipal.Current
        //        .FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
        //    try
        //    {
        //        var servicePointUri = new Uri(_graphResourceId);
        //        var serviceRoot = new Uri(servicePointUri, tenantId);
        //        var activeDirectoryClient = new ActiveDirectoryClient(serviceRoot,
        //            async () => await GetTokenForApplication());

        //        // use the token for querying the graph to get the user details

        //        var result = await activeDirectoryClient.Users
        //            .Where(u => u.ObjectId.Equals(userObjectId))
        //            .ExecuteAsync();
        //        var user = result.CurrentPage.ToList().First();

        //        return user.DisplayName;
        //    }
        //    // if the above failed, the user needs to explicitly re-authenticate for the app to obtain the required token
        //    catch (Exception e)
        //    {
        //        return e.Message;
        //    }
        //}

        public async Task<IUser> UserDetails()
        {
            var tenantId = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid")
               .Value;
            var userObjectId = ClaimsPrincipal.Current
                .FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            try
            {
                var servicePointUri = new Uri(_graphResourceId);
                var serviceRoot = new Uri(servicePointUri, tenantId);
                var activeDirectoryClient = new ActiveDirectoryClient(serviceRoot,
                    async () => await GetTokenForApplication());

                // use the token for querying the graph to get the user details

                var result = await activeDirectoryClient.Users
                    .Where(u => u.ObjectId.Equals(userObjectId))
                    .ExecuteAsync();
                var user = result.CurrentPage.ToList().First();

                return user;
            }
            // if the above failed, the user needs to explicitly re-authenticate for the app to obtain the required token
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> AdGroupCheck(IUser user)
        {

            try
            {
                IEnumerable<string> groups = await user.GetMemberGroupsAsync(false);
                int cnt = 0;
                IUserFetcher retrievedUserFetcher = (User)user;
                IPagedCollection<IDirectoryObject> pagedCollection = retrievedUserFetcher.MemberOf.ExecuteAsync().Result;
                do
                {
                    List<IDirectoryObject> directoryObjects = pagedCollection.CurrentPage.ToList();
                    foreach (IDirectoryObject directoryObject in directoryObjects)
                    {
                        if (directoryObject.ObjectId == AllowedGroupID || directoryObject.ObjectId == MyAdGroup)
                        {
                            cnt++;

                        }
                    }
                    pagedCollection = pagedCollection.GetNextPageAsync().Result;
                } while (pagedCollection != null && pagedCollection.MorePagesAvailable);

                if (cnt < 1)
                {
                    return false;
                }

                return true;
            }
            // if the above failed, the user needs to explicitly re-authenticate for the app to obtain the required token
            catch (Exception e)
            {
                return false;
            }
        }


        public async Task<string> GetTokenForApplication()
        {
            var signedInUserId = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            var tenantId = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;
            var userObjectId = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

            // get a token for the Graph without triggering any user interaction (from the cache, via multi-resource refresh token, etc)
            var clientcred = new ClientCredential(_clientId, _appKey);
            // initialize AuthenticationContext with the token cache of the currently signed in user, as kept in the app's database
            var authenticationContext = new AuthenticationContext(_aadInstance + tenantId, new ADALTokenCache(signedInUserId));
            var authenticationResult = await authenticationContext.AcquireTokenSilentAsync(_graphResourceId, clientcred, new UserIdentifier(userObjectId, UserIdentifierType.UniqueId));
            return authenticationResult.AccessToken;
        }
    }
}