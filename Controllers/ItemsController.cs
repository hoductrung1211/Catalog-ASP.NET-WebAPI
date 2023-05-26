using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.Entities;
// Add this one first, so the intellisense can suggest ControllerBase for use y

namespace Catalog.Controllers
{
  
    [ApiController]
    [Route("Items")]
    // By default, what you would put here is just the name of the controller
    public class ItemsController: ControllerBase
    {
        private readonly InMemItemsRepository repostiory;
        public ItemsController()
        {
            repostiory = new InMemItemsRepository();
        }

        // GET /items 
        [HttpGet]
        public IEnumerable<Item> GetItems()
        {
            var items = repostiory.GetItems();
            return items;
        }

        // GET /item/{id}
        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(Guid id)
        {
            var item = repostiory.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }

            return item;
        }
    }
}