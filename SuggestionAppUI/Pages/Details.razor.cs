using Microsoft.AspNetCore.Components;

namespace SuggestionAppUI.Pages;

public partial class Details
{
    [Parameter]
    public string Id { get; set; }

    private SuggestionModel suggestion;
    private UserModel loggedInUser;
    private List<StatusModel> statuses;
    private string settingStatus = "";
    private string urlText = "";
    protected async override Task OnInitializedAsync()
    {
        suggestion = await suggestionData.GetSuggestion(Id);
        loggedInUser = await authProvider.GetUserFromAuthentication(userData);
        statuses = await statusData.GetStatusesAsync();
    }

    private void ClosePage()
    {
        navManager.NavigateTo("/");
    }

    private async Task VoteUp()
    {
        if (loggedInUser is not null)
        {
            if (suggestion.Author.Id == loggedInUser.Id)
            {
                // Can't vote on your own suggestion
                return;
            }

            if (suggestion.UserVotes.Add(loggedInUser.Id) == false)
            {
                suggestion.UserVotes.Remove(loggedInUser.Id);
            }

            await suggestionData.UpvoteSuggestion(suggestion.Id, loggedInUser.Id);
        }
        else
        {
            navManager.NavigateTo("/MicrosoftIdentity/Account/SignIn", true);
        }
    }

    private string GetUpvoteTopText()
    {
        string output;
        if (suggestion.UserVotes?.Count > 0)
        {
            output = suggestion.UserVotes.Count.ToString("00");
        }
        else
        {
            output = "Click to";
        }

        return output;
    }

    private string GetUpvoteBottomText()
    {
        string output;
        if (suggestion.UserVotes?.Count > 1)
        {
            output = "Upvotes";
        }
        else
        {
            output = "Upvote";
        }

        return output;
    }

    private string GetVoteClass()
    {
        if (suggestion.UserVotes is null || suggestion.UserVotes.Count == 0)
        {
            return "suggestion-detail-no-votes";
        }
        else if (suggestion.UserVotes.Contains(loggedInUser?.Id))
        {
            return "suggestion-detail-voted";
        }
        else
        {
            return "suggestion-detail-not-voted";
        }
    }

    private string GetStatusClass()
    {
        if (suggestion is null || suggestion.Status is null)
        {
            return "suggestion-detail-status-none";
        }

        string output = suggestion.Status.Name switch
        {
            "Completed" => "suggestion-detail-status-completed",
            "Watching" => "suggestion-detail-status-watching",
            "Upcoming" => "suggestion-detail-status-upcoming",
            "Dismissed" => "suggestion-detail-status-dismissed",
            _ => "suggestion-detail-status-none",
        };
        return output;
    }

    private async Task SetStatus()
    {
        switch (settingStatus)
        {
            case "completed":
                if (string.IsNullOrWhiteSpace(urlText))
                {
                    return;
                }

                suggestion.Status = statuses.Where(s => s.Name.ToLower() == settingStatus.ToLower()).First();
                suggestion.OwnerNotes = $"You are right, this is an important topic for devs. We created a resource about it at: <a href='{urlText}' target='_blank'>{urlText}</a>";
                break;
            case "watching":
                suggestion.Status = statuses.Where(s => s.Name.ToLower() == settingStatus.ToLower()).First();
                suggestion.OwnerNotes = $"We noticed the interest in this suggestion. If more people vote we may address this topic.";
                break;
            case "upcoming":
                suggestion.Status = statuses.Where(s => s.Name.ToLower() == settingStatus.ToLower()).First();
                suggestion.OwnerNotes = $"Great suggestion. We are about to address this topic.";
                break;
            case "dismissed":
                suggestion.Status = statuses.Where(s => s.Name.ToLower() == settingStatus.ToLower()).First();
                suggestion.OwnerNotes = $"We cannot do it right now.";
                break;
            default:
                return;
        }

        settingStatus = null;
        await suggestionData.UpdateSuggestion(suggestion);
    }
}