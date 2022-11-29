using System.Linq.Expressions;
using MongoDB.Driver;
using Play.Common.Contracts.Interfaces;

namespace Play.Common.Data.Repositories
{

    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly IMongoCollection<T> _dbCollection;

        private readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            // var mongoClient = new MongoClient("mongodb://localhost:27017");
            // var database = mongoClient.GetDatabase("Catalog");
            _dbCollection = database.GetCollection<T>(collectionName);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await _dbCollection.Find(_filterBuilder.Empty).ToListAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbCollection.Find(filter).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(i => i.Id, id);
            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            await _dbCollection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            var filter = _filterBuilder.Eq(i => i.Id, entity.Id);
            await _dbCollection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(i => i.Id, id);
            await _dbCollection.DeleteOneAsync(filter);
        }
    }
}