using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VisitorCounterAPI.Models
{
    public class Visitor
    {
        public Guid Id { get; set; }
        public int Visits { get; set; }
    }
}
