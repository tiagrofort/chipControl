namespace ChipControl.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class ChipControlDbContextFactory : IDesignTimeDbContextFactory<ChipControlDbContext>
{
    public ChipControlDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ChipControlDbContext>();
        optionsBuilder.UseSqlite("Data Source=chipcontrol.db");
        return new ChipControlDbContext(optionsBuilder.Options);
    }
}
