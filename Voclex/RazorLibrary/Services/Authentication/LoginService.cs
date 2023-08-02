using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using RazorLibrary.Services.Interfaces;
using SharedLibrary.DataTransferObjects.Authentication;

namespace RazorLibrary.Services.Authentication;

public sealed class LoginService
{
	private readonly IAppHttpClient appHttpClient;
	private readonly IAppStorage storage;
	private readonly NavigationManager navigation;
	private readonly ClientJwtAuthenticationStateProvider authenticationStateProvider;

	public LoginService(IAppHttpClient appHttpClient, IAppStorage storage, NavigationManager navigation, ClientJwtAuthenticationStateProvider authenticationStateProvider)
	{
		this.appHttpClient = appHttpClient;
		this.storage = storage;
		this.navigation = navigation;
		this.authenticationStateProvider = authenticationStateProvider;
	}

	public async Task<bool> Login(LoginModel user)
	{
		using var msg = await appHttpClient.ApiClient.PostAsJsonAsync("/login", user);

		if (!msg.IsSuccessStatusCode)
		{
			return false;
		}

		var result = await msg.Content.ReadFromJsonAsync<LoginResult>();

		await storage.SetItem("token", result!.Token);

		authenticationStateProvider.NotifyAuthenticationStateChanged();

		navigation.NavigateTo("/");

		return true;
	}
}

