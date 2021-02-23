using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MessengerAPI.Models;
using MessengerAPI.Managers;
using System;
using System.Text;
using System.Security.Claims;

namespace MessengerAPI.Handler
{
    public class AuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private UserManager _userManager;

        public AuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            UserManager manager)
            : base(options, logger, encoder, clock)
        {
            _userManager = manager;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization") && !Request.Query.ContainsKey("access_token"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            string token = !Request.Headers.ContainsKey("Authorization") ? Request.Query["access_token"] : Request.Headers["Authorization"];
            User user = null;

            try
            {
                var credentialBytes = Convert.FromBase64String(token);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                var email = credentials[0];
                var password = credentials[2];
                user = await _userManager.AuthenticateUser(email, password);
            }
            catch (Exception e)
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            if (user == null)
                return AuthenticateResult.Fail("Invalid Username or Password");

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            Context.User = principal;

            return AuthenticateResult.Success(ticket);
        }
    }
}