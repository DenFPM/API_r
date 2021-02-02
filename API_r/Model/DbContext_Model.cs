using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API_r.Model
{
    public class DbContext_Model : DbContext
    {
        public DbContext_Model(DbContextOptions<DbContext_Model> options) : base (options)
        {

        }
        public DbSet<Project> projects { get; set; }
    }
}
