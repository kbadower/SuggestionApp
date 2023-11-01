namespace SuggestionAppLibrary.Models;

public class BasicUserModel
{
    [BsonRepresentation(BsonType.ObjectId)]
    public int Id { get; set; }
    public string DisplayName { get; set; }

    public BasicUserModel()
    {
        
    }

    public BasicUserModel(UserModel user)
    {
        DisplayName = user.DisplayName;
    }
}
