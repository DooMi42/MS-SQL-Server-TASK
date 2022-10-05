using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Harjotus03
{
    public class Pelitietokanta: DbContext
    {
        public DbSet<Login>? Logins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = "Data Source=.;" +
                "Initial Catalog=Pelitietokanta;" +
                "Integrated Security=true;" +
                "MultipleActiveResultSets=true;";
                optionsBuilder.UseSqlServer(connection);
        }
    }
}
