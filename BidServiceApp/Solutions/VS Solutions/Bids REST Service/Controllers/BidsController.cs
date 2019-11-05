using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bid_REST_Service.Persistency;

namespace Bid_REST_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {        
        // GET
        [HttpGet]
        public IEnumerable<Bid> Get()
        {
            return PersistencyService.Get();                
        }

      
        // GET by ID
        [HttpGet("{id}", Name = "Get")]
        public Bid Get(string id)
        {
            return PersistencyService.Get(id);
        }

        // POST
        [HttpPost]
        public void Post([FromBody] Bid value)
        {            
            PersistencyService.Post(value);
        }

        // PUT
        [HttpPut]
        [Route("{id}")]
        public void Put(string id, [FromBody] Bid value)
        {
            PersistencyService.Put(PersistencyService.Get(id).Id, value);            
        }
            
        // DELETE
        [HttpDelete]
        [Route("{id}")]
        public void Delete(string id)
        {
            PersistencyService.Delete(id);
        }

            

        
    }
}
