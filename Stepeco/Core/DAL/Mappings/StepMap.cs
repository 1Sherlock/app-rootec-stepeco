using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stepeco.Core.DAL.Entities;

namespace Stepeco.Core.DAL.Mappings
{
    public class StepMap : IEntityTypeConfiguration<Step>
    {
        public void Configure(EntityTypeBuilder<Step> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
