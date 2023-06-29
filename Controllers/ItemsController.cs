using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.Dtos;
using Catalog.Entities;
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

        // GET /items/{id}
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

        // POST /items/
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = new Guid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            repostiory.CreateItem(item);
            return CreatedAtAction(nameof(GetItem), new { id = item.Id}, item.AsDto());
        }

        // PUT /items/
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = repostiory.GetItem(id);
            
            if (existingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with {
                Name = itemDto.Name,
                Price = itemDto.Price,
            };

            repostiory.UpdateItem(updatedItem);

            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = repostiory.GetItem(id);
            
            if (existingItem is null)
            {
                return NotFound();
            }

            repostiory.DeleteItem(existingItem);

            return NoContent();
        }
    }
}