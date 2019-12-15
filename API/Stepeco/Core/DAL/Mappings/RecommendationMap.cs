using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stepeco.Core.DAL.Entities;

namespace Stepeco.Core.DAL.Mappings
{
    public class RecommendationMap : IEntityTypeConfiguration<Recommendation>
    {
        public void Configure(EntityTypeBuilder<Recommendation> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Maximum)
                   .IsRequired(false);

            builder.Property(p => p.Minimum)
                   .IsRequired(false);
        }
    }
}
