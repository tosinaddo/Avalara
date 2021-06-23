using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalara.Models.Contracts.Response;
using Avalara.Models.Contracts.Request;
using Avalara.Common;

namespace Avalara.Interfaces
{
    public interface ITodoService
    {
        Task<ServiceResponse<List<TodoItem>>> GetTodos();
        Task<ServiceResponse<TodoItem>> GetTodoItem(Guid guid);
        Task<ServiceResponse<List<TodoItem>>> AddTodoItems(AddTodo todos);
        Task<ServiceResponse<List<TodoItem>>> UpdateTodos(UpdateTodo todos);
        Task<ServiceResponse<SuccessDetails>> DeleteTodos(DeleteTodo todos);
        Task<ServiceResponse<List<TaskItem>>> GetTasks();
        Task<ServiceResponse<TaskItem>> GetTaskItem(int id);
        Task<ServiceResponse<List<TaskItem>>> AddTasks(AddTask task);
        Task<ServiceResponse<List<TaskItem>>> UpdateTasks(UpdateTask todo);
        Task<ServiceResponse<SuccessDetails>> DeleteTasks(DeleteTask task);
    }
}
