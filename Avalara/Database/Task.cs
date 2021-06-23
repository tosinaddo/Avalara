using System;
using System.Collections.Generic;

#nullable disable

namespace Avalara.Database
{
    public partial class Task
    {
        public int TaskId { get; set; }
        public Guid TodoListId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public string Status { get; set; }

        public virtual TodoList TodoList { get; set; }
    }
}
