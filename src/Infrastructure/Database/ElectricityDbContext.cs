using Domain.Counter;
using Microsoft.EntityFrameworkCore;
using Domain.Common.Database.Abstractions;

namespace Infrastructure.Database;

public class ElectricityDbContext : DbContext
{
    public ElectricityDbContext(DbContextOptions<ElectricityDbContext> options) : base(options) { }

    public DbSet<HouseholdMeteringPlant> HouseholdMeteringPlants { get; set; }
}