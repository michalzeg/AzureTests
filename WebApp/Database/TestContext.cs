using Microsoft.EntityFrameworkCore;

namespace WebApp.Database
{
    public class TestContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Test> Tests { get; set; }


        public TestContext(DbContextOptions<TestContext> options) : base(options)
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
