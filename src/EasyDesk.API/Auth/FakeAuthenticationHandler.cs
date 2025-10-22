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

public class FakeAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public FakeAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        System.Text.Encodings.Web.UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    { }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var headers = Request.Headers;
        var claims = new List<Claim>();

        if(headers.TryGetValue("X-User-Id", out var idHeader) && int.TryParse(idHeader.FirstOrDefault(), out var userId))
        {
            var userService = Context.RequestServices.GetService<IUserService>();
            if (userService != null)
            {
                var user = await userService.GetUserByIdAsync(userId);
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