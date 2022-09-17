using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IDServer
{
    public class IdentityConfiguration
    {
        public static List<TestUser> TestUsers =>
    new List<TestUser>
    {
        new TestUser
        {
            SubjectId = "111",
            Username = "arunoda89",
            Password = "123",
            Claims =
            {
                new Claim(JwtClaimTypes.Name, "Arunoda Wijewantha"),
                new Claim(JwtClaimTypes.GivenName, "Arunoda"),
                new Claim(JwtClaimTypes.FamilyName, "Wijewantha"),
                new Claim(JwtClaimTypes.WebSite, "http://google.com"),
            }
        }
};
        public static IEnumerable<IdentityResource> IdentityResources =>
    new IdentityResource[]
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
    };

        public static IEnumerable<ApiScope> ApiScopes =>
    new ApiScope[]
    {
        new ApiScope("identityTestApi.read"),
        new ApiScope("identityTestApi.write"),
    };

        public static IEnumerable<ApiResource> ApiResources =>
    new ApiResource[]
    {
        new ApiResource("identityTestApi")
        {
            Scopes = new List<string>{ "identityTestApi.read","identityTestApi.write" },
            ApiSecrets = new List<Secret>{ new Secret("arunodaApiSecret".Sha256()) }
        }
    };

        public static IEnumerable<Client> Clients =>
    new Client[]
    {
        new Client
        {
            ClientId = "cwm.client",
            ClientName = "Weather Forecast Client",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { new Secret("arunodaClientSecret".Sha256()) },
            AllowedScopes = { "identityTestApi.read" }
        },
    };
    }
}
