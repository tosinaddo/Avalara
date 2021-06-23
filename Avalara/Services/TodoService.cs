using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalara.Interfaces;
using Avalara.Models.Contracts.Response;
using Avalara.Models.Contracts.Request;
using Avalara.Database;
using Avalara.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Avalara.Services
{
    public class TodoService : ITodoService
    {
        private readonly AvalaraContext _cntx;
        public TodoService(AvalaraContext context)
        {
            _cntx = context;
        }
        public async Task<ServiceResponse<List<TodoItem>>> GetTodos()
        {
            ServiceResponse<List<TodoItem>> response;

            var result = await _cntx.TodoLists.Select(t => new TodoItem { TodoId = t.TodoListId, CreateDate = t.CreateDate, Name = t.Name }).ToListAsync();
            if (result is not null)
            {
                response = new ServiceResponse<List<TodoItem>>
                {
                    Success = true,
                    ResponseData = result
                };
            }
            else
            {
                response = new ServiceResponse<List<TodoItem>>
                {
                    Success = false,
                    ErrorDetails = new ProblemDetails
                    {
                        Status = 404,
                        Title = "Todo Item not found",
                        Detail = $"Todo item exists"
                    }
                };
            }
            return response;
        }
        public async Task<ServiceResponse<TodoItem>> GetTodoItem(Guid guid)
        {
            ServiceResponse<TodoItem> response;

            var result = await _cntx.TodoLists.Where(t => t.TodoListId == guid).Select(t => new TodoItem { TodoId = t.TodoListId, CreateDate = t.CreateDate, Name = t.Name }).FirstOrDefaultAsync();
            if (result is not null)
            {
                response = new ServiceResponse<TodoItem>
                {
                    Success = true,
                    ResponseData = result
                };
            }
            else
            {
                response = new ServiceResponse<TodoItem>
                {
                    Success = false,
                    ErrorDetails = new ProblemDetails 
                    {
                        Status = 404,
                        Title = "Todo Item not found",
                        Detail = $"Todo item with id '{guid}' does not exists"
                    }
                };
            }
            return response;
        }
        public async Task<ServiceResponse<List<TodoItem>>> AddTodoItems(AddTodo todos)
        {
            ServiceResponse<List<TodoItem>> response;
            
            //create list of todo to add to the database
            var todosDB = todos.Names.Select(name => new TodoList { Name = name }).ToList();
            //add
            _cntx.TodoLists.AddRange(todosDB);
            try
            {
                await _cntx.SaveChangesAsync();
                response = new ServiceResponse<List<TodoItem>>
                { 
                    Success = true, 
                    ResponseData = todosDB.Select(t => new TodoItem { Name = t.Name, CreateDate = t.CreateDate, TodoId = t.TodoListId }).ToList() 
                };
            }
            catch (Exception)
            {
                response = new ServiceResponse<List<TodoItem>>
                {
                    Success = false,
                    ErrorDetails = new ProblemDetails { Status = 500, Title = "Error saving", Detail = "Error saving"}
                };
            }

            return response;
        }
        public async Task<ServiceResponse<List<TodoItem>>> UpdateTodos(UpdateTodo todos)
        {
            ServiceResponse<List<TodoItem>> response;

            //get all todos form the database
            var todoIds = todos.items.Select(t => t.TodoId).ToList();
            var todosDB = await _cntx.TodoLists.Where(t => todoIds.Contains(t.TodoListId)).ToListAsync();
            //if no items are found in the db retun not found
            if(!todosDB.Any())
            {
                response = new ServiceResponse<List<TodoItem>>
                {
                    Success = false,
                    ErrorDetails = new ProblemDetails
                    {
                        Status = 404,
                        Title = "Todo Item not found",
                        Detail = $"Todo Items not found"
                    }
                };

                return response;
            }
            var result = todos.items.Join(todosDB, t => t.TodoId, s => s.TodoListId, (t, s) => new { todo = t, todoDB = s });
            foreach(var item in result)
            {
                item.todoDB.Name = item.todo.Name;
            }
            //save changes to the database
            try
            {
                await _cntx.SaveChangesAsync();
                response = new ServiceResponse<List<TodoItem>>
                {
                    Success = true,
                    ResponseData = todosDB.Select(t => new TodoItem { Name = t.Name, CreateDate = t.CreateDate, TodoId = t.TodoListId }).ToList()
                };
            }
            catch (Exception)
            {
                response = new ServiceResponse<List<TodoItem>>
                {
                    Success = false,
                    ErrorDetails = new ProblemDetails { Status = 500, Title = "Error saving", Detail = "Error saving" }
                };
            }

            return response;
        }
        public async Task<ServiceResponse<SuccessDetails>> DeleteTodos(DeleteTodo todos)
        {
            ServiceResponse<SuccessDetails> response;

            //get all todos form the database
            var todoIds = todos.TodoIds;
            var todosDB = await _cntx.TodoLists.Where(t => todoIds.Contains(t.TodoListId)).Include(t => t.Tasks).ToListAsync();
            //if no items are found in the db retun not found
            if (!todosDB.Any())
            {
                response = new ServiceResponse<SuccessDetails>
                {
                    Success = false,
                    ErrorDetails = new ProblemDetails
                    {
                        Status = 404,
                        Title = "Todo Item not found",
                        Detail = $"Todo Items not found"
                    }
                };

                return response;
            }

            foreach(var item in todosDB)
            {
                _cntx.Tasks.RemoveRange(item.Tasks);
                _cntx.TodoLists.Remove(item);
            }
            //savechanges
            try
            {
                await _cntx.SaveChangesAsync();
                response = new ServiceResponse<SuccessDetails>
                {
                    Success = true,
                    ResponseData = new SuccessDetails 
                    { 
                        Status = 200,
                        Message = $"The following Todos with listed ids were deleted: {string.Join(",",todosDB.Select(t => t.TodoListId).ToArray())}"
                    }
                };
            }
            catch (Exception)
            {
                response = new ServiceResponse<SuccessDetails>
                {
                    Success = false,
                    ErrorDetails = new ProblemDetails { Status = 500, Title = "Error saving", Detail = "Error saving" }
                };
            }

            return response;
        }
        public async Task<ServiceResponse<List<TaskItem>>> GetTasks()
        {
            ServiceResponse<List<TaskItem>> response;

            var result = await _cntx.Tasks.Select(t => new TaskItem 
            {
                TaskId = t.TaskId,
                CreateDate = t.CreateDate,
                TodoListId = t.TodoListId,
                Subject = t.Subject,
                Description = t.Description,
                Status = t.Status
            }).ToListAsync();

            if (result is not null)
            {
                response = new ServiceResponse<List<TaskItem>>
                {
                    Success = true,
                    ResponseData = result
                };
            }
            else
            {
                response = new ServiceResponse<List<TaskItem>>
                {
                    Success = false,
                    ErrorDetails = new ProblemDetails
                    {
                        Status = 404,
                        Title = "Task Item not found",
                        Detail = $"Task item don't exists"
                    }
                };
            }
            return response;
        }
        public async Task<ServiceResponse<TaskItem>> GetTaskItem(int id)
        {
            ServiceResponse<TaskItem> response;

            var result = await _cntx.Tasks.Where(t => t.TaskId == id).Select(t => new TaskItem
            {
                TaskId = t.TaskId,
                CreateDate = t.CreateDate,
                TodoListId = t.TodoListId,
                Subject = t.Subject,
                Description = t.Description,
                Status = t.Status
            }).FirstOrDefaultAsync();

            if (result is not null)
            {
                response = new ServiceResponse<TaskItem>
                {
                    Success = true,
                    ResponseData = result
                };
            }
            else
            {
                response = new ServiceResponse<TaskItem>
                {
                    Success = false,
                    ErrorDetails = new ProblemDetails
                    {
                        Status = 404,
                        Title = "Task Item not found",
                        Detail = $"Task item with id '{id}' does not exists"
                    }
                };
            }
            return response;
        }
        public async Task<ServiceResponse<List<TaskItem>>> AddTasks(AddTask task)
        {
            ServiceResponse<List<TaskItem>> response;
            //create the list of task items to be added to the database
            var TasksDB = task.Items
                .SelectMany(t => t.GroupItems, (t, s) => new Avalara.Database.Task 
                {
                    Subject = s.Subject, 
                    Description = s.Description, 
                    Status = s.Status, 
                    TodoListId = t.TodoListId 
                }).ToList();
            //save items to the database
            try
            {
                _cntx.Tasks.AddRange(TasksDB);
                await _cntx.SaveChangesAsync();
                response = new ServiceResponse<List<TaskItem>>
                {
                    Success = true,
                    ResponseData = TasksDB.Select(t => new TaskItem 
                    { 
                       CreateDate = t.CreateDate,
                       Description = t.Description,
                       Status = t.Status,
                       Subject = t.Subject,
                       TaskId = t.TaskId,
                       TodoListId = t.TodoListId
                    }).ToList()
                };
            }
            catch (Exception e)
            {

                response = new ServiceResponse<List<TaskItem>>
                {
                    Success = false,
                    ErrorDetails = new ProblemDetails { Status = 500, Title = "Error saving", Detail = $"Error saving {e.Message}" }
                };
            }

            return response;
        }
        public async Task<ServiceResponse<List<TaskItem>>> UpdateTasks(UpdateTask task)
        {
            ServiceResponse<List<TaskItem>> response;
            //obtain ids of all items that need to be deleted
            var taskIds = task.Items.Select(t => t.TaskId).ToList();
            //obtain all items from the database
            var tasksDB = await _cntx.Tasks.Where(t => taskIds.Contains(t.TaskId)).ToListAsync();
            //if not DB item foound return error message
            if (!tasksDB.Any())
            {
                response = new ServiceResponse<List<TaskItem>>
                {
                    Success = false,
                    ErrorDetails = new ProblemDetails
                    {
                        Status = 404,
                        Title = "Todo Item not found",
                        Detail = $"Todo Items not found"
                    }
                };

                return response;
            }
            //update the task returned
            var result = task.Items.Join(tasksDB, t => t.TaskId, s => s.TaskId, (s, t) => new { task = t, taskDB = s });

            foreach(var item in result)
            {
                item.taskDB.Status = item.task.Status;
                item.taskDB.Description = item.task.Description;
                item.taskDB.Subject = item.task.Subject;
            }
            //save changes to the database
            try
            {
                await _cntx.SaveChangesAsync();
                response = new ServiceResponse<List<TaskItem>>
                {
                    Success = true,
                    ResponseData = tasksDB.Select(t => new TaskItem 
                    { 
                        TaskId = t.TaskId, 
                        CreateDate = t.CreateDate, 
                        TodoListId = t.TodoListId,
                        Subject = t.Subject,
                        Description = t.Description,
                        Status = t.Status
                    }).ToList()
                };
            }
            catch (Exception)
            {
                response = new ServiceResponse<List<TaskItem>>
                {
                    Success = false,
                    ErrorDetails = new ProblemDetails { Status = 500, Title = "Error saving", Detail = "Error saving" }
                };
            }

            return response;
        }
        public async Task<ServiceResponse<SuccessDetails>> DeleteTasks(DeleteTask task)
        {
            ServiceResponse<SuccessDetails> response;

            //get all todos form the database
            var taskIds = task.TaskIds;
            var tasksDB = await _cntx.Tasks.Where(t => taskIds.Contains(t.TaskId)).ToListAsync();
            //if no items are found in the db retun not found
            if (!tasksDB.Any())
            {
                response = new ServiceResponse<SuccessDetails>
                {
                    Success = false,
                    ErrorDetails = new ProblemDetails
                    {
                        Status = 404,
                        Title = "Todo Item not found",
                        Detail = $"Todo Items not found"
                    }
                };

                return response;
            }

            foreach (var item in tasksDB)
            {
                _cntx.Tasks.Remove(item);
            }
            //savechanges
            try
            {
                await _cntx.SaveChangesAsync();
                response = new ServiceResponse<SuccessDetails>
                {
                    Success = true,
                    ResponseData = new SuccessDetails
                    {
                        Status = 200,
                        Message = $"The following Tasks with listed ids were deleted: {string.Join(",", tasksDB.Select(t => t.TaskId).ToArray())}"
                    }
                };
            }
            catch (Exception)
            {
                response = new ServiceResponse<SuccessDetails>
                {
                    Success = false,
                    ErrorDetails = new ProblemDetails { Status = 500, Title = "Error saving", Detail = "Error saving" }
                };
            }

            return response;
        }
    }
}
