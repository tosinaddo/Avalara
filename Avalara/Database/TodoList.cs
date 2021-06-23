using System;
using System.Collections.Generic;

#nullable disable

namespace Avalara.Database
{
    public partial class TodoList
    {
        public TodoList()
        {
            Tasks = new HashSet<Task>();
        }

        public Guid TodoListId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
