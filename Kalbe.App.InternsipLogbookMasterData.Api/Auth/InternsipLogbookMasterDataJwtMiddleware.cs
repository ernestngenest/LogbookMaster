using Kalbe.Library.Common.Auth;
using Microsoft.Extensions.Caching.Distributed;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Kalbe.App.InternsipLogbookMasterData.Api.Auth
{
    public class InternsipLogbookMasterDataJwtMiddleware : JwtMiddleware
    {
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _cache;

        public InternsipLogbookMasterDataJwtMiddleware(RequestDelegate next, IConfiguration configuration, IDistributedCache cache) : base(next, configuration)
        {
            _configuration = configuration;
            _cache = cache;
        }

        protected override void AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                // current apikey format: apikey-[consuming]-[consumer]
                if (token.StartsWith("apikey", StringComparison.InvariantCultureIgnoreCase))
                {
                    var appConsumer = token.Split('-').Last();
                    List<Claim> claims = new()
                    {
                        new Claim("UserName", appConsumer),
                        new Claim("Name", appConsumer)
                    };
                    context.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
                }
                else
                {

                    if (Utils.IsTokenValid(token, _configuration.GetSection("AppJwtSecret").Value, out JwtSecurityToken validToken) && context != null)
                    {
                        context.User = new ClaimsPrincipal(new ClaimsIdentity(validToken.Claims));
                    }
                }
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
