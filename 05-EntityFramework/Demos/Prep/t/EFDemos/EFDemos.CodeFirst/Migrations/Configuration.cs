namespace EFDemos.CodeFirst.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EFDemos.CodeFirst.PositionsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "EFDemos.CodeFirst.PositionsContext";
        }

        protected override void Seed(EFDemos.CodeFirst.PositionsContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

          context.Positions.AddOrUpdate(
              p => new { p.Latitude, p.Longitude },
              new Position() { Latitude = 33, Longitude = -112, ReportedAt = DateTime.Now },
              new Position() { Latitude = 34, Longitude = -113, ReportedAt = DateTime.Now },
              new Position() { Latitude = 35, Longitude = -114, ReportedAt = DateTime.Now },
              new Position() { Latitude = 36, Longitude = -115, ReportedAt = DateTime.Now }
            );
        }
    }
}
