using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCad.Examples.FurnitureConfigurator.DAL
{
    public class FurnitureDbContext : DbContext
    {
        public virtual DbSet<DbPanel> Panels { get; set; }
        public virtual DbSet<DbDoor> Doors { get; set; }
        public virtual DbSet<DbDrawer> Drawers { get; set; }
        public virtual DbSet<DbHandle> Handles { get; set; }
        public virtual DbSet<DbFrame> Frames { get; set; }

        public FurnitureDbContext(string connectionStr) : base(connectionStr)
        {
        }
    }
}
