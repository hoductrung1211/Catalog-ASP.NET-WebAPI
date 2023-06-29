using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.Dtos;
// Add this one first, so the intellisense can suggest ControllerBase for use y

namespace Catalog.Controllers
{
    [ApiController]
    [Route("items")]
    // By default, what you would put here is just the name of the controller
    public class ItemsController: ControllerBase
    {
        private readonly IItemsRepository repostiory;
        public ItemsController(IItemsRepository repostiory)
        {
            this.repostiory = repostiory;
        }

        // GET /items 
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            var items = repostiory.GetItems().Select( item => item.AsDto());
            return items;
        }

        // GET /item/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repostiory.GetItem(id);

            if (item is null)
            {
                return NotFound();
            }

            return item.AsDto();
        }
    }
}