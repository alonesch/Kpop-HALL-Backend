using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KpopHall.Infrastructure.Persistence;

public class KpopHallDbContextFactory : IDesignTimeDbContextFactory<KpopHallDbContext>
{
    public KpopHallDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<KpopHallDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS05;Database=kpophalltest;Trusted_Connection=True;TrustServerCertificate=True;");
        return new KpopHallDbContext(optionsBuilder.Options);
    }
}