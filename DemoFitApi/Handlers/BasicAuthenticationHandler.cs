using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;

namespace DemoFitApi.Handlers;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
	private Settings settings= new Settings();
	public BasicAuthenticationHandler(
		IOptionsMonitor<AuthenticationSchemeOptions> options,
		ILoggerFactory logger,
		UrlEncoder encoder,
		ISystemClock clock
		) : base(options, logger, encoder, clock)
	{
	}

	protected override Task<AuthenticateResult> HandleAuthenticateAsync()
	{
		if (settings.EnableUserAgent)
		{
			var agentHeader = Request.Headers["User-Agent"].ToString();
			if (!agentHeader.Equals(settings.UserAgentKey))
				return Task.FromResult(AuthenticateResult.Fail("Invalid User Agent"));
		}

		if(settings.EnableAuthorization)
		{
			var authHeader = Request.Headers["Authorization"].ToString();
			if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
			{

				var token = authHeader.Substring("Basic ".Length).Trim();
				System.Console.WriteLine(token);
				var credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(token));
				var credentials = credentialstring.Split(':');
				if (credentials[0] == settings.Authorization_username && credentials[1] == settings.Authorization_password)
				{
					var claims = new[] { new Claim("name", credentials[0]), new Claim(ClaimTypes.Role, "Admin") };
					var identity = new ClaimsIdentity(claims, "Basic");
					var claimsPrincipal = new ClaimsPrincipal(identity);
					return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
				}

				Response.StatusCode = 401;
				Response.Headers.Add("WWW-Authenticate", "Basic realm=\"dotnetthoughts.net\"");
				return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
			}
			else
			{
				Response.StatusCode = 401;
				Response.Headers.Add("WWW-Authenticate", "Basic realm=\"dotnetthoughts.net\"");
				return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
			}

		} else
		{
			var claims = new[] { new Claim("admin", "admin"), new Claim(ClaimTypes.Role, "Admin") };
			var identity = new ClaimsIdentity(claims, "Basic");
			var claimsPrincipal = new ClaimsPrincipal(identity);
			return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
		}

			



	}
}
