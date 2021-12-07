using States_ms.Models;
using States_ms.Contexts;

namespace States_ms.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using MongoDB.Bson;
    using System.Linq;
    public class StateRepository : IStateRepository
    {
        private readonly IStateContext _context;
        public StateRepository(IStateContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<State>> GetAllStates()
        {
            return await _context.States.Find(_ => true).ToListAsync();
        }
        public Task<State> GetState(long id)
        {
            FilterDefinition<State> filter = Builders<State>.Filter.Eq(m => m.stateId, id);
            return _context
                    .States
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }
        public async Task Create(State state)
        {
            await _context.States.InsertOneAsync(state);
        }
        public async Task<bool> Update(State state)
        {
            ReplaceOneResult updateResult =
                await _context
                        .States
                        .ReplaceOneAsync(
                            filter: g => g.stateId == state.stateId,
                            replacement: state);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> Delete(long id)
        {
            FilterDefinition<State> filter = Builders<State>.Filter.Eq(m => m.stateId, id);
            DeleteResult deleteResult = await _context
                                                .States
                                              .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
        public async Task<long> GetNextId()
        {
            return await _context.States.CountDocumentsAsync(new BsonDocument()) + 1;
        }
    }
}