using BlazorBlogApp2.Server.Extensions;
using BlazorBlogApp2.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorBlogApp2.Server.Services
{
    public class ToDoService
    {
        private readonly ToDoListContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _user;

        public ToDoService(IHttpContextAccessor httpContextAccessor)
        {
            _context = new ToDoListContext();

            _httpContextAccessor = httpContextAccessor;

            _user = _httpContextAccessor.HttpContext.User ?? null;
        }

        public async Task<IList<ToDoItem>> GetDoLists()
        {
            return await _context.ToDoLists
                .AsNoTracking()
                .Where(x=> x.UserId == int.Parse(_user.GetValue(ClaimTypes.NameIdentifier)))
                .ToListAsync();
        }

        public async Task<ToDoItem> AddToDo(ToDoItem todo)
        {
            todo.UserId = int.Parse(_user.GetValue(ClaimTypes.NameIdentifier));

            await _context.ToDoLists.AddAsync(todo);

            await _context.SaveChangesAsync();

            return todo;
        }

        public async Task<ToDoItem> UpdateToDo(int id, ToDoItem todo)
        {
            var item = await _context.ToDoLists.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null) return null;

            item.Item = todo.Item;

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task DeleteToDo(int id)
        {
            var item = await _context.ToDoLists.FirstOrDefaultAsync(x => x.Id == id);

            if (item != null)
            {
                _context.Remove(item);

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAll()
        {
            var items = await _context.ToDoLists.ToListAsync();

            _context.RemoveRange(items);

            await _context.SaveChangesAsync();
        }
    }
}
