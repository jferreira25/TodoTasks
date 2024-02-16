using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Tasks.Domain.Entities;

public class Tasks : DataMappingBase<Guid>
{
    public Tasks() => Id = Guid.NewGuid();

    [BsonElement("body")]
    public string Body { get; set; }

    [BsonElement("title")]
    public string Title { get; set; }

    [BsonElement("createdDate")]
    [BsonIgnoreIfNull]
    public DateTime? CreatedDate { get; private set; }

    [BsonElement("completedDate")]
    [BsonIgnoreIfNull]
    public DateTime? CompletedDate { get; set; }

    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    public TasksStatus Status { get; set; }

    [BsonElement("deleted")]
    public bool Deleted { get; private set; }

    public void SetDelete()
    {
        Deleted = true;
    }

    public void SetCreatedDate()
    {
        CreatedDate = DateTime.Now;
    }
}