using MongoDB.Bson.Serialization.Attributes;

public abstract class DataMappingBase<TIdentifier>
{
    [BsonElement("id")]
    public virtual TIdentifier Id { get; set; }
}