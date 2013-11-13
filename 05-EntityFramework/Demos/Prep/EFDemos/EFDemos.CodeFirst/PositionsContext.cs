using System.Data.Entity;

namespace EFDemos.CodeFirst
{
  public class PositionsContext : DbContext
  {
    public DbSet<Position> Positions { get; set; }
  }
}
