namespace SuggestionAppLibrary.DataAccess
{
    public interface ISuggestionData
    {
        Task CreateSuggestion(SuggestionModel suggestion);
        Task<List<SuggestionModel>> GetApprovedSuggestionsAsync();
        Task<SuggestionModel> GetSuggestion(string id);
        Task<List<SuggestionModel>> GetSuggestionsAsync();
        Task<List<SuggestionModel>> GetWaitingForApprovalSuggestions();
        Task UpdateSuggestion(SuggestionModel suggestion);
        Task UpvoteSuggestion(string suggestionId, string userId);
    }
}