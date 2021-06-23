using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Avalara.Common
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public T ResponseData { get; set; }
        public ProblemDetails ErrorDetails { get; set; }
    }
}
