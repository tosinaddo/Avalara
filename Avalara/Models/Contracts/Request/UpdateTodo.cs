using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalara.Models.Contracts.Response;
using System.ComponentModel.DataAnnotations;

namespace Avalara.Models.Contracts.Request
{
    public class UpdateTodoItem
    { 
        public string Name { get; set; }
        public Guid TodoId { get; set; }
    }
    public class UpdateTodo
    {
        public List<UpdateTodoItem> items { get; set; }
    }
}
