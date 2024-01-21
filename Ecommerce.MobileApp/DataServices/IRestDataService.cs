using Ecommerce.MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.MobileApp.DataServices
{
    public interface IRestDataService
    {

        Task<List<Todo>> GetAllTodosAsync();

        Task AddTodoAsync(Todo item);

        Task UpdateTodo(Todo item);

        Task DeleteTodoAsync(int  id);
    }
}
