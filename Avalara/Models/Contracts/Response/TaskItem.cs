using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avalara.Models.Contracts.Response
{
    public class TaskItem
    {
        public int TaskId { get; set; }
        public Guid TodoListId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public string Status { get; set; }
    }
}
