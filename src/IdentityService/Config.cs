using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        ];

    public static IEnumerable<ApiScope> ApiScopes =>
        [
            new ApiScope("auctionApp", "Auction app full access"),
        ];

    public static IEnumerable<Client> Clients =>
        [
            // m2m client credentials flow client
            // new Client
            // {
            //     ClientId = "m2m.client",
            //     ClientName = "Client Credentials Client",

            //     AllowedGrantTypes = GrantTypes.ClientCredentials,
            //     ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

            //     AllowedScopes = { "scope1" }
            // },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "postman",
                ClientName = "Postman",
                AllowedScopes = {"openid", "profile", "auctionApp"},
                RedirectUris = {"http://www.getpostman.com/oauth2/callback"},
                ClientSecrets = [new Secret("NotASecret".Sha256())],
                AllowedGrantTypes = {GrantType.ResourceOwnerPassword}
            },
        ];
}
