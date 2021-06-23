using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avalara.Models.Contracts.Request
{
    public class UpdateTaskItem
    {
        public int TaskId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
    public class UpdateTask
    {
        public List<UpdateTaskItem> Items { get; set; }
    }
}
