using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace haymatlosApi.Utils
{
    public class JwtService
    {
        IServiceCollection _services;
        private const string tokenSecret = "this_will_also_change_later";
        public JwtService(IServiceCollection services) 
        {
            _services = services;
        }

        public void configureServices()
        {
            var key = Encoding.ASCII.GetBytes(tokenSecret);

            _services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
        //https://docs.appeon.com/snapdevelop2019/Secure_a_Web_API_with_JWT_Token/index.html
    }
}
