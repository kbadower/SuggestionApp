namespace SuggestionAppUI.Pages;

public partial class AdminApproval
{
    private List<SuggestionModel> submissions = new();
    private SuggestionModel editingModel;
    private string currentEditingTitle = "";
    private string editedTitle = "";
    private string currentEditingDescription = "";
    private string editedDescription = "";
    protected async override Task OnInitializedAsync()
    {
        submissions = await suggestionData.GetWaitingForApprovalSuggestions();
    }

    private async Task ApproveSuggestion(SuggestionModel suggestion)
    {
        suggestion.IsApprovedForRelease = true;
        submissions.Remove(suggestion);
        await suggestionData.UpdateSuggestion(suggestion);
    }

    private async Task RejectSuggestion(SuggestionModel suggestion)
    {
        suggestion.IsRejected = true;
        submissions.Remove(suggestion);
        await suggestionData.UpdateSuggestion(suggestion);
    }

    private void EditTitle(SuggestionModel suggestion)
    {
        editingModel = suggestion;
        editedTitle = suggestion.Suggestion;
        currentEditingTitle = suggestion.Id;
        currentEditingDescription = "";
    }

    private async Task SaveTitle(SuggestionModel suggestion)
    {
        currentEditingTitle = string.Empty;
        suggestion.Suggestion = editedTitle;
        await suggestionData.UpdateSuggestion(suggestion);
    }

    private void EditDescription(SuggestionModel suggestion)
    {
        editingModel = suggestion;
        editedDescription = suggestion.Description;
        currentEditingTitle = "";
        currentEditingDescription = suggestion.Id;
    }

    private async Task SaveDescription(SuggestionModel suggestion)
    {
        currentEditingDescription = string.Empty;
        suggestion.Description = editedDescription;
        await suggestionData.UpdateSuggestion(suggestion);
    }

    private void ClosePage()
    {
        navManager.NavigateTo("/");
    }
}