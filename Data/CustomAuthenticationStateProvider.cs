using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Almacen.Data;
using System.Collections.Concurrent;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly AuthenticationService _authService;
    private string _authenticatedUserEmail;

    public CustomAuthenticationStateProvider(AuthenticationService authService)
    {
        _authService = authService;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsIdentity identity;

        if (!string.IsNullOrEmpty(_authenticatedUserEmail))
        {
            identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, _authenticatedUserEmail) }, "custom");
        }
        else
        {
            identity = new ClaimsIdentity();
        }

        var user = new ClaimsPrincipal(identity);
        return Task.FromResult(new AuthenticationState(user));
    }

    public async Task<bool> MarkUserAsAuthenticated(string email, string password)
    {
        var result = await _authService.Login(email, password);
        if (result)
        {
            _authenticatedUserEmail = email;  // Almacenamos el email del usuario autenticado.
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return true;
        }
        return false;
    }

    public void MarkUserAsLoggedOut()
    {
        _authenticatedUserEmail = null; // Limpiamos el email del usuario autenticado.
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
