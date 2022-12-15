using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServingSystem.Models;

namespace ServingSystem.Data
{
    public class ServingSystemContext : DbContext
    {
        public ServingSystemContext (DbContextOptions<ServingSystemContext> options)
            : base(options)
        {
        }

        public DbSet<ServingSystem.Models.ProductViewModel> ProductViewModel { get; set; }
    }
}
