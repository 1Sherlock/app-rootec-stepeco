using Microsoft.EntityFrameworkCore;
using Stepeco.Core.DAL.Entities;
using Stepeco.Core.DAL.Mappings;

namespace Stepeco.Core.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EnvironmentRecord> EnvironmentRecords { get; set; }
        public DbSet<Step> Steps { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new EnvironmentRecordMap());
            builder.ApplyConfiguration(new StepMap());
        }
    }
}
