using States_ms.Models;
using States_ms.Contexts;
using System;

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
            state.createdOn = DateTime.UtcNow;
            state.active = true;
            state.stateId = await GetNextId();
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

        public async Task<IEnumerable<string>> GetMediaId(string userId)
        {
            List<State> states = await _context.States.Find(m => m.userId == userId).ToListAsync();
            List<string> response = new List<string>();
            foreach (var e in states)
            {
                response.Add(e.mediaId);
            }
            return response;
        }
        public async Task<IEnumerable<StatesDTO>> GetMedia(string userId)
        {
            List<State> states = await _context.States.Find(m => (m.userId == userId && m.active) && m.createdOn > DateTime.UtcNow.AddHours(-24) ).ToListAsync();
            List<StatesDTO> response = new List<StatesDTO>();
            foreach (var e in states)
            {
                 if (e.internalId.CreationTime.CompareTo(DateTime.UtcNow.AddHours(-24)) == 1){
                     response.Add( new StatesDTO (e));
                 }
            }
            return response;
        }
        public async Task<IEnumerable<StatesDTO>> GetStatesImage(List<String> usersId)
        {
            List<State> states = await _context.States.Find(m => m.active && usersId.Contains(m.userId) && m.createdOn > DateTime.UtcNow.AddHours(-24)).SortByDescending(e => e.createdOn).ToListAsync();
            List<StatesDTO> response = new List<StatesDTO>();
            foreach (var e in states)
            {
                 if (usersId.Contains(e.userId)){
                     usersId.RemoveAll( x => x == e.userId);
                     response.Add( new StatesDTO (e));
                 }
            }
            return response;
        }

        public async Task<IEnumerable<StatesDTO>> GetAllActive(List<String> usersId)
        {
            List<State> states = await _context.States.Find(m => m.active && usersId.Contains(m.userId) && m.createdOn > DateTime.UtcNow.AddHours(-24)).SortByDescending(e => e.createdOn).ToListAsync();
            List<StatesDTO> response = new List<StatesDTO>();
            foreach (var e in states)            {
                response.Add( new StatesDTO (e));
            }
            return response;
        }
    }
}