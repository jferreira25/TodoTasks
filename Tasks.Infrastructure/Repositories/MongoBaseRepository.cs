using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Tasks.Infrastructure.Repositories;

public abstract class MongoBaseRepository<TEntity, TIdentifier> where TEntity : DataMappingBase<TIdentifier>, new()
{
    private readonly IMongoDatabase _database;
    private readonly string _collectionName;
    private readonly string _databaseName;
    protected IMongoDatabase Database => _database;
    public virtual Func<TEntity, FilterDefinition<TEntity>> FilterEntity { get; } = null;

    public MongoBaseRepository(
        string connection,
        string databaseName,
        string collectionName,
        string partitionKey = null)
    {
        _collectionName = collectionName;
        _databaseName = databaseName;
        RegisterClassMap();
        var mongoClient = new MongoClient(connection);
        _database = mongoClient.GetDatabase(databaseName);
        DefinePartitionKey(partitionKey);
    }

    public virtual async Task AddAsync(TEntity item)
    {
        var collection = _database.GetCollection<TEntity>(_collectionName);
        await collection.InsertOneAsync(item);
    }

    public virtual async Task UpdateAsync(TEntity item, dynamic updateFields = null)
    {
        var collection = _database.GetCollection<TEntity>(_collectionName);

        await collection.ReplaceOneAsync(x => x.Id.Equals(item.Id), item, new ReplaceOptions { IsUpsert = true });
    }

    public virtual async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> whereClause)
    {
        var collection = _database.GetCollection<TEntity>(_collectionName);
        var filterDef = Builders<TEntity>.Filter.And(new[] {
                    Builders<TEntity>.Filter.Where(whereClause)
                });

        var result = await collection.Find(filterDef).Limit(1).SingleOrDefaultAsync();

        return result;
    }

    public virtual async Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> whereClause)
    {
        var collection = _database.GetCollection<TEntity>(_collectionName);
        var filterDef = Builders<TEntity>.Filter.And(new[] {
                            Builders<TEntity>.Filter.Where(whereClause)
                        });

        var result = await collection.FindAsync(filterDef);

        return result.ToList();
    }

    private static void RegisterClassMap()
    {
        if (!BsonClassMap.IsClassMapRegistered(typeof(TEntity)))
        {
            BsonClassMap.RegisterClassMap<TEntity>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
    }

    private void DefinePartitionKey(string partitionKey)
    {
        if (!string.IsNullOrEmpty(partitionKey))
        {
            var bson = new BsonDocument
                {
                    {"shardCollection", $"{_databaseName}.{_collectionName}"},
                    {"key", new BsonDocument(partitionKey, "hashed")}
                };

            _database.RunCommand(new BsonDocumentCommand<BsonDocument>(bson));
        }
    }
}