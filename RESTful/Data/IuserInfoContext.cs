#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RESTful.Models;

namespace RESTful.Data
{
    public class IuserInfoContext : DbContext
    {
        public IuserInfoContext (DbContextOptions<IuserInfoContext> options)
            : base(options)
        {
        }

        public DbSet<RESTful.Models.UserInfo> UserInfo { get; set; }
    }
}
