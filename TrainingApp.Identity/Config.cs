﻿using Duende.IdentityServer.Models;

namespace TrainingApp.Identity;

public static class Config
{
	public static IEnumerable<IdentityResource> IdentityResources =>
		new IdentityResource[]
		{
			new IdentityResources.OpenId()
		};

	public static IEnumerable<ApiScope> ApiScopes =>
		new List<ApiScope>
		{
			new ApiScope("api1", "My API")
		};

	public static IEnumerable<Client> Clients =>
		new List<Client>
		{
			new Client
			{
				ClientId = "client",

				AllowedGrantTypes = GrantTypes.ClientCredentials,

				ClientSecrets =
				{
					new Secret("secret".Sha256())
				},
				
				RedirectUris = { "https://localhost:5001/signin-oidc" },

				AllowedScopes = { "api1" }
			}
		};
}