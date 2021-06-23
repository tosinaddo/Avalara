using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalara.Models.Contracts.Response;
using Avalara.Models.Contracts.Request;
using System.ComponentModel.DataAnnotations;

namespace Avalara.Models.Contracts.Request
{
    public class AddTodo
    {
        [Required]
        public List<string> Names{ get; set; }
    }
}
