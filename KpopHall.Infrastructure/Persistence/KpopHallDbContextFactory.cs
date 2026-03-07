using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KpopHall.Infrastructure.Persistence;

public class KpopHallDbContextFactory : IDesignTimeDbContextFactory<KpopHallDbContext>
{
    public KpopHallDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<KpopHallDbContext>();
        optionsBuilder.UseSqlServer("Server=CRISTIAN\\SQLEXPRESS;Database=kpophalltest;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True;Connection Timeout=30;");
        return new KpopHallDbContext(optionsBuilder.Options);
    }
}