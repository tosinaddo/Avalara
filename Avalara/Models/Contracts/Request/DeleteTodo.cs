using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avalara.Models.Contracts.Request
{
    public class DeleteTodo
    {
        public List<Guid> TodoIds { get; set; }
    }
}
