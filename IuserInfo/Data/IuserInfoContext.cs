#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IuserInfo.Models;

namespace IuserInfo.Data
{
    public class IuserInfoContext : DbContext
    {
        public IuserInfoContext (DbContextOptions<IuserInfoContext> options)
            : base(options)
        {
        }

        public DbSet<IuserInfo.Models.Info> Info { get; set; }
    }
}
