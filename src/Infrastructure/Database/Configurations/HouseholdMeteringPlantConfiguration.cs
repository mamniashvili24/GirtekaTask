using Domain.Counter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class HouseholdMeteringPlantConfiguration : IEntityTypeConfiguration<HouseholdMeteringPlant>
{
    public void Configure(EntityTypeBuilder<HouseholdMeteringPlant> builder)
    {
        builder.Property(o => o.TINKLAS).HasMaxLength(64);
     
        builder.Property(o => o.OBJ_GV_TIPAS).HasMaxLength(16);
        
        builder.Property(o => o.OBT_PAVADINIMAS).HasMaxLength(32);
    }
}