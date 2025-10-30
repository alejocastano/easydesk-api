/*
    This is a fake authentication handler used for testing purposes.
    It retrieves an user and his roles from the database based on a custom header. ("X-User-Id")
    and sets the claims accordingly.

    NOTE: This should be deleted when real authenticatio via JWT or similar is implemented.
*/

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using EasyDesk.Application;
using EasyDesk.Domain;

public class JwtAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IUserService _userService;
    public JwtAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        System.Text.Encodings.Web.UrlEncoder encoder,
        ISystemClock clock,
        IUserService userService)
        : base(options, logger, encoder, clock)
    {
        _userService = userService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var headers = Request.Headers;
        var claims = new List<Claim>();

        if(headers.TryGetValue("X-User-Id", out var idHeader) && int.TryParse(idHeader.FirstOrDefault(), out var userId))
        {
            if (_userService != null)
            {
                var user = await _userService.GetUserByIdAsync(userId);
                if (user != null)
                {
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    if (!string.IsNullOrEmpty(user.Username))
                    {
                        claims.Add(new Claim(ClaimTypes.Name, user.Username));
                    }
                    if (!string.IsNullOrEmpty(user.Email))
                    {
                        claims.Add(new Claim(ClaimTypes.Email, user.Email));
                    }

                    foreach (var ur in user.UserRoles ?? Enumerable.Empty<UserRole>())
                    {
                        if (ur?.Role != null && !string.IsNullOrEmpty(ur.Role.Name))
                        {
                            claims.Add(new Claim(ClaimTypes.Role, ur.Role.Name));
                        }
                    }
                }
            }
        }

        if (!claims.Any())
        {
            return AuthenticateResult.Fail("Invalid or missing X-User-Id header");
        }

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}