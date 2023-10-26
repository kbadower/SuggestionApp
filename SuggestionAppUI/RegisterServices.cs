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
    }
}
