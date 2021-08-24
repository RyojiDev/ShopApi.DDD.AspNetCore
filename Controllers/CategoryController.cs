using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopApi.DDD.AspNetCore.Model;

namespace ShopApi.DDD.AspNetCore.Controllers
{
    [Route("category")]
    public class CategoryController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get()
        {
            return await Task.FromResult(new List<Category>());
        }

        [Route("{id:int}")]
        [HttpGet]
        public string GetById(int id)
        {
            return "Olá mundo " + id;
        }

        [HttpPost]
        public async Task<Category> Post([FromBody] Category model)
        {
            return await Task.FromResult(model);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult<Category>> Put(int id, [FromBody] Category model )
        {
            if( model.Id == id)
                return Ok(model);
            return null;    
        }
    }
}