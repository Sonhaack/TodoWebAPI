using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class TodoService : ITodosService
    {
        private string todoFile = "todos.json";
        private IList<Todo> todos;

        public TodoService()
        {
            if (!File.Exists(todoFile))
            {
                Seed();
                WriteTodosToFile();
            }
            else
            {
                string content = File.ReadAllText(todoFile);
                todos = JsonSerializer.Deserialize<List<Todo>>(content);
            }
        }

        private void Seed()
        {
            Todo[] ts =
            {
                new Todo
                {
                    userId = 1,
                    id = 1,
                    title = "Do dishes",
                    completed = false
                },
                new Todo
                {
                    userId = 1,
                    id = 2,
                    title = "Walk the dog",
                    completed = false
                },
                new Todo
                {
                    userId = 2,
                    id = 3,
                    title = "Do DNP homework",
                    completed = true
                },
                new Todo
                {
                    userId = 3,
                    id = 4,
                    title = "Eat breakfast",
                    completed = false
                },
                new Todo
                {
                    userId = 4,
                    id = 5,
                    title = "Mow the lawn",
                    completed = true
                },
            };
            todos = ts.ToList();
        }

        public async Task<IList<Todo>> GetTodosAsync()
        {
            List<Todo> tmp = new List<Todo>(todos);
            return tmp;
        }

        public async Task AddTodoAsync(Todo todo)
        {
            int max = todos.Max(todo => todo.id);
            todo.id = (++max);
            todos.Add(todo);
            WriteTodosToFile();
        }

        private void WriteTodosToFile()
        {
            string productsAsJson = JsonSerializer.Serialize(todos);
            File.WriteAllText(todoFile, productsAsJson);
        }

        public async Task RemoveTodoAsync(int todoId)
        {
            Todo todoToRemove = todos.First(t => t.id == todoId);
            todos.Remove(todoToRemove);
           WriteTodosToFile();
        }

        public async Task UpdateAsync(Todo todo)
        {
            Todo toUpdate = todos.First(t => t.id == todo.id);
            toUpdate.completed = todo.completed;
            WriteTodosToFile();
        }
    }
}
