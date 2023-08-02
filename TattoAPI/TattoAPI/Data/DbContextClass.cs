using TattoAPI.Models;
using Microsoft.EntityFrameworkCore;
using TattoAPI.Models.Avigma;

namespace TattoAPI.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("Conn_dBcon"));
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDisplay> UsersDisplay { get; set; }
        public DbSet<UserMaster_DTO> userMasters { get; set; }
    }
}
