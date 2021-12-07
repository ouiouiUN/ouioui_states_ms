
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using States_ms.Models;
using States_ms.Repositories;

namespace States_ms.Controllers
{
    using States_ms.Models;
    using States_ms.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class StatesController: Controller
    {
        private readonly IStateRepository _repo;
        public StatesController(IStateRepository repo)
        {
            _repo = repo;
        }
        // GET api/states
        [HttpGet]
        public async Task<ActionResult<IEnumerable<State>>> Get()
        {   
            return new ObjectResult(await _repo.GetAllStates());
        }
        // GET api/states/1
        [HttpGet("{id}")]
        public async Task<ActionResult<State>> Get(long id)
        {
            var state = await _repo.GetState(id);
            if (state == null)
                return new NotFoundResult();
            
            return new ObjectResult(state);
        }
        // POST api/states
        [HttpPost]
        public async Task<ActionResult<State>> Post([FromBody] State state)
     
        {   
            state.stateId = await _repo.GetNextId();
            await _repo.Create(state);
            return new OkObjectResult(state);
        }
        // PUT api/states/1
        [HttpPut("{id}")]
        public async Task<ActionResult<State>> Put(long id, [FromBody] State state)
        {
            var stateFromDb = await _repo.GetState(id);
            if (stateFromDb == null)
                return new NotFoundResult();
            state.stateId = stateFromDb.stateId;
            state.internalId = stateFromDb.internalId;
            await _repo.Update(state);
            return new OkObjectResult(state);
        }
        // DELETE api/states/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var post = await _repo.GetState(id);
            if (post == null)
                return new NotFoundResult();
            await _repo.Delete(id);
            return new OkResult();
        }
    }
}