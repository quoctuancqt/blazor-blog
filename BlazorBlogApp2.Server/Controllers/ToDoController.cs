using BlazorBlogApp2.Server.Services;
using BlazorBlogApp2.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorBlogApp2.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoService _toDoService;

        public ToDoController(ToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _toDoService.GetDoLists();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ToDoItem item)
        {
            var result = await _toDoService.AddToDo(item);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] ToDoItem item)
        {
            var result = await _toDoService.UpdateToDo(id, item);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _toDoService.DeleteToDo(id);

            return NoContent();
        }

        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAll()
        {
            await _toDoService.DeleteAll();

            return NoContent();
        }
    }
}
