using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Data
{
    public interface ITodosService
        {
            Task <IList<Todo>> GetTodosAsync();
            Task  AddTodoAsync(Todo todo);
            Task  RemoveTodoAsync(int todoId);
            Task UpdateAsync(Todo todo);
        } 
}
