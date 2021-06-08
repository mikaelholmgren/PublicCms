using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VisitorCounterAPI.Models;

namespace VisitorCounterAPI.Data
{
    public class VisitorCounterAPIContext : DbContext
    {
        public VisitorCounterAPIContext (DbContextOptions<VisitorCounterAPIContext> options)
            : base(options)
        {
        }

        public DbSet<VisitorCounterAPI.Models.Visitor> Visitor { get; set; }
    }
}
