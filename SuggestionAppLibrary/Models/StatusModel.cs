namespace SuggestionAppLibrary.Models;

public class StatusModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
