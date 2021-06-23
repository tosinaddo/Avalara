using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avalara.Models.Contracts.Request
{
    public class AddTaskItem
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
    public class AddTaskItemGroup
    {
        public Guid TodoListId { get; set; }
        public List<AddTaskItem> GroupItems { get; set; }
    }
    public class AddTask
    {
        public List<AddTaskItemGroup> Items { get; set; }
    }
}
