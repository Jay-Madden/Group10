using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace Group10.Auth
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("Group10Api", "Group10APi", 
                    new[] {"Admin", "Driver", "Sponsor"})
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                /*
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "Group10Api" }
                },
                */
                new Client
                {
                    ClientId = "Group10",
                    ClientName = "Group10Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,

                    RedirectUris =           { "https://localhost:5003/callback.html" },
                    PostLogoutRedirectUris = { "https://localhost:5003/index.html" },
                    //AllowedCorsOrigins =     { "https://localhost:5003" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "Group10Api"
                    }
                }
            };
    }
}