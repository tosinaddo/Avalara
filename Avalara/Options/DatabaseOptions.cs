using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avalara.Options
{
    public class DatabaseOptions
    {
        public const string Database = "AvalaraDB";
        public string Server { get; set; }
        public string Catlog { get; set; }
    }
}
