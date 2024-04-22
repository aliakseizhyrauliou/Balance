using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Balance.BePaid.Web.Infrastructure;


public static class AuthExtension
{
    private const string BearerSchema = "Bearer";
    
    public static void AddAuth(this WebApplicationBuilder builder)
    {
        IdentityModelEventSource.ShowPII = true;

        builder.Services.AddAuthentication(BearerSchema)
            .AddJwtBearer(options =>
            {
                options.Authority = builder.Configuration["IdentityAuthority"];
                options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = false,
                    ValidateAudience = false
                };
            });
    }
}
