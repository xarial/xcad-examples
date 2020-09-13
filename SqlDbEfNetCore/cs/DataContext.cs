using Microsoft.EntityFrameworkCore;

namespace SqlDbEfNetCore
{
    public class DataContext : DbContext
    {
        private readonly string m_ConnectionString;

        public DataContext(string connectionString) : base()
        {
            m_ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(m_ConnectionString);
            }
        }

        public DbSet<Parts> Parts { get; set; }
    }
}
