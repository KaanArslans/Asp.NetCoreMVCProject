using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Base
{
    public abstract class Record
    {
        public int id { get; set; }
        public string? Guid { get; set; }
    }
}
