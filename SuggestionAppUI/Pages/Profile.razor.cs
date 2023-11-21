namespace SuggestionAppUI.Pages;

public partial class Profile
{
    private UserModel loggedInUser;
    private List<SuggestionModel> submissions;
    private List<SuggestionModel> approved;
    private List<SuggestionModel> archived;
    private List<SuggestionModel> pending;
    private List<SuggestionModel> rejected;
    protected async override Task OnInitializedAsync()
    {
        loggedInUser = await authProvider.GetUserFromAuthentication(userData);
        var results = await suggestionData.GetUserSuggestions(loggedInUser.Id);
        if (loggedInUser is not null && results is not null)
        {
            submissions = results.OrderByDescending(s => s.DateCreated).ToList();
            approved = submissions.Where(s => s.IsApprovedForRelease == true && s.IsArchived == false && s.IsRejected == false).ToList();
            archived = submissions.Where(s => s.IsArchived && s.IsRejected == false).ToList();
            pending = submissions.Where(s => s.IsApprovedForRelease == false && s.IsRejected == false).ToList();
            rejected = submissions.Where(s => s.IsRejected).ToList();
        }
    }

    private void ClosePage()
    {
        navManager.NavigateTo("/");
    }
}