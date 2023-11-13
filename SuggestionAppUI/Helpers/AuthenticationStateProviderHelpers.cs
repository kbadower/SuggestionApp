using Microsoft.AspNetCore.Components.Authorization;

namespace SuggestionAppUI.Helpers
{
    public static class AuthenticationStateProviderHelpers
    {
        public static async Task<UserModel> GetUserFromAuthentication(this AuthenticationStateProvider authProdiver, IUserData userData)
        {
            var authState = await authProdiver.GetAuthenticationStateAsync();
            string objectId = authState.User.Claims.FirstOrDefault(c => c.Type.Contains("objectidentifier"))?.Value;
            return await userData.GetUserFromAuthentication(objectId);
        }
    }
}
