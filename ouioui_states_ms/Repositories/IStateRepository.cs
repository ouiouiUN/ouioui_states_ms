using States_ms.Models;

namespace States_ms.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IStateRepository
    {
        // api/[GET]
        Task<IEnumerable<State>> GetAllStates();
        // api/1/[GET]
        Task<State> GetState(long id);
        // api/[POST]
        Task Create(State state);
        // api/[PUT]
        Task<bool> Update(State state);
        // api/1/[DELETE]
        Task<bool> Delete(long id);
        Task<long> GetNextId();

        // api/states/media
         Task<IEnumerable<string>> GetMediaId(string userId);
        
        // api/states/getmedia
        Task<IEnumerable<StatesDTO>> GetMedia(string userId);

        // api/states/preview
        Task<IEnumerable<StatesDTO>> GetStatesImage(List<string> userId);

        // api/states/getAll
        Task<IEnumerable<StatesDTO>> GetAllActive(List<string> userId);
    }
}