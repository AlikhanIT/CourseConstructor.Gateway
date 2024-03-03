using CourseConstructors.CourseConstructors.Core.Interfaces.Persistance;
using Microsoft.EntityFrameworkCore;

namespace CourseConstructors.CourseConstructors.Infrastructure.Persistance;

public class Context : DbContext, IContext
{
    public Context(DbContextOptions<Context> options) 
        :base(options)
    {
        Database.EnsureCreated();
    }
}