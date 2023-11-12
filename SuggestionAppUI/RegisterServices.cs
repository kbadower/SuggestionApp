using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using MongoDB.Driver;

namespace SuggestionAppUI
{
    public static class RegisterServices
    {
        public static void RegisterCoreBlazorServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddMemoryCache();
        }

        public static void RegisterSuggestionDataServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IDbConnection, DbConnection>();
            builder.Services.AddSingleton<ICategoryData, MongoCategoryData>();
            builder.Services.AddSingleton<IUserData, MongoUserData>();
            builder.Services.AddSingleton<IStatusData, MongoStatusData>();
            builder.Services.AddSingleton<ISuggestionData, MongoSuggestionData>();
        }

        public static void RegisterAuthServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireClaim("JobTitle", "Admin");
                });
            });
        }
    }
}
