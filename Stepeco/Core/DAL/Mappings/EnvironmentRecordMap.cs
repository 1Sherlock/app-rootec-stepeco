using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stepeco.Core.DAL.Entities;

namespace Stepeco.Core.DAL.Mappings
{
    public class EnvironmentRecordMap : IEntityTypeConfiguration<EnvironmentRecord>
    {
        public void Configure(EntityTypeBuilder<EnvironmentRecord> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Temperature)
                   .IsRequired(false);

            builder.Property(p => p.Pressure)
                   .IsRequired(false);

            builder.Property(p => p.Humidity)
                   .IsRequired(false);

            builder.Property(p => p.NoiseLevel)
                   .IsRequired(false);

            builder.Property(p => p.Quality)
                   .IsRequired(false);
        }
    }
}
