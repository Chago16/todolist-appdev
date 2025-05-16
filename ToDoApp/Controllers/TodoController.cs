using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace ToDoApp.Controllers
{
    public class TodoController : Controller
    {
       
        private static List<TodoItem> todoList = new List<TodoItem>();

        
        public IActionResult Index(string categoryFilter = null, string priorityFilter = null, string sortBy = null, bool showHidden = false)
        {
            var todos = todoList.AsEnumerable();

            if (!string.IsNullOrEmpty(categoryFilter))
                todos = todos.Where(t => t.Category == categoryFilter);

            if (!string.IsNullOrEmpty(priorityFilter))
                todos = todos.Where(t => t.Priority == priorityFilter);

            if (sortBy == "priority")
            {
                todos = todos.OrderBy(t => GetPriorityOrder(t.Priority));
            }
            else if (sortBy == "category")
                todos = todos.OrderBy(t => t.Category);

        
            if (!showHidden)
                todos = todos.Where(t => !t.IsHidden);

            return View(todos.ToList());
        }

        private int GetPriorityOrder(string priority)
        {
            return priority switch
            {
                "High" => 1,
                "Medium" => 2,
                "Low" => 3,
                _ => 4  // Default for unexpected values
            };
        }

       
        [HttpPost]
        public IActionResult Add(string title, string category, string priority)
        {
            var newTodo = new TodoItem
            {
                Id = todoList.Count > 0 ? todoList.Max(t => t.Id) + 1 : 1,
                Title = title,
                Category = category,
                Priority = priority,
                IsCompleted = false
            };
            todoList.Add(newTodo);
            return RedirectToAction("Index");
        }

      
        [HttpPost]
        public IActionResult ToggleComplete(int id)
        {
            var todo = todoList.FirstOrDefault(t => t.Id == id);
            if (todo != null)
                todo.IsCompleted = !todo.IsCompleted;

            return RedirectToAction("Index");
        }

       
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = todoList.FirstOrDefault(t => t.Id == id);
            if (item != null)
                todoList.Remove(item);

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult ToggleHide(int id)
        {
            var todo = todoList.FirstOrDefault(t => t.Id == id);
            if (todo != null)
                todo.IsHidden = !todo.IsHidden;

            return RedirectToAction("Index");
        }
    }
}
