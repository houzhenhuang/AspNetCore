using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _todoContext;

        public TodoController(TodoContext todoContext)
        {
            this._todoContext = todoContext;

            if (!_todoContext.TodoItems.Any())
            {
                _todoContext.TodoItems.Add(new TodoItem { Name = "Item1" });
                _todoContext.SaveChanges();
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> GetAll()
        {
            return await _todoContext.TodoItems.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public async Task<ActionResult<TodoItem>> GetById(long id)
        {
            var item =await  _todoContext.TodoItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoItem item)
        {
            await _todoContext.TodoItems.AddAsync(item);
            await _todoContext.SaveChangesAsync();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, TodoItem item)
        {
            var todo = await _todoContext.TodoItems.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _todoContext.TodoItems.Update(todo);
            await _todoContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var todo =await _todoContext.TodoItems.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _todoContext.TodoItems.Remove(todo);
            await _todoContext.SaveChangesAsync();
            return NoContent();
        }
    }
}