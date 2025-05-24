using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UMS.Core.Entities;

namespace UMS.Infrastructure.Data
{
    public class UmsDbContext:DbContext
    {
        public UmsDbContext(DbContextOptions<UmsDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
