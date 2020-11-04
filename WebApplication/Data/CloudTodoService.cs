using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication.Models;

namespace Todo_tutorial_2._0.Data
{
    public class CloudTodoService : ITodosService
    {
        
        HttpClient client = new HttpClient();
        
        public async Task<IList<Todo>> GetTodosAsync()
        {
            
            string message = await client.GetStringAsync("https://jsonplaceholder.typicode.com/todos");
            List<Todo> result = JsonSerializer.Deserialize<List<Todo>>(message);
            return result;
        }

        public async Task AddTodoAsync(Todo todo)
        {
            string todoAsJson = JsonSerializer.Serialize(todo);
            HttpContent content = new StringContent(todoAsJson,Encoding.UTF8, "application/json");
             await client.PostAsync("http://jsonplaceholder.typicode.com"+"/todos", content);
        }

        public async Task RemoveTodoAsync(int todoId)
        {
            string url = "http://jsonplaceholder.typicode.com";
            await client.DeleteAsync($"{url}/todos/{todoId}");
        }

        public async Task UpdateAsync(Todo todo)
        {
            string todoAsJson = JsonSerializer.Serialize(todo);
            HttpContent content = new StringContent(todoAsJson,Encoding.UTF8,"application/json");
            await client.PatchAsync($"{"http://jsonplaceholder.typicode.com"}/todos/{todo.id}", content);
        }
    }
}