using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Avalara.Models.Contracts.Response
{
    public class TodoItem
    {
        public Guid TodoId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
