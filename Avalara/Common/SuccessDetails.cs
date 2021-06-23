using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Avalara.Common
{
    public class SuccessDetails
    {
        public string Message { get; set; }
        public int Status { get; set; } = StatusCodes.Status200OK;
    }
}
