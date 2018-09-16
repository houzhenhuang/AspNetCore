﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoApiSample.Core;
using TodoApiSample.Core.Interfaces;
using TodoApiSample.Core.Model;

namespace TodoApiSample.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ILogger _logger;

        public TodoController(ITodoRepository todoRepository,
            ILogger<TodoController> logger)
        {
            _todoRepository = todoRepository;
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            using (_logger.BeginScope("Message {HoleValue}", DateTime.Now))
            {
                _logger.LogInformation(LoggingEvents.ListItems, "Listing all items");
                EnsureItems();
            }
            return _todoRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(string id)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Getting item {ID}", id);
            var item = _todoRepository.Find(id);
            if (item == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "GetById({ID}) NOT FOUND", id);
                return NotFound();
            }
            return new ObjectResult(item);
        }
        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _todoRepository.Add(item);
            _logger.LogInformation(LoggingEvents.InsertItem, "Item {ID} Created", item.Key);
            return CreatedAtRoute("GetTodo", new { controller = "Todo", id = item.Key }, item);
        }
         [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] TodoItem item)
        {
            if (item == null || item.Key != id)
            {
                return BadRequest();
            }

            var todo = _todoRepository.Find(id);
            if (todo == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "Update({ID}) NOT FOUND", id);
                return NotFound();
            }

            _todoRepository.Update(item);
            _logger.LogInformation(LoggingEvents.UpdateItem, "Item {ID} Updated", item.Key);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _todoRepository.Remove(id);
            _logger.LogInformation(LoggingEvents.DeleteItem, "Item {ID} Deleted", id);
        }

        private void EnsureItems()
        {
            if (!_todoRepository.GetAll().Any())
            {
                _logger.LogInformation(LoggingEvents.GenerateItems, "Generating sample items.");
                for (int i = 1; i < 11; i++)
                {
                    _todoRepository.Add(new TodoItem() { Name = "Item " + i });
                }
            }
        }
    }
}