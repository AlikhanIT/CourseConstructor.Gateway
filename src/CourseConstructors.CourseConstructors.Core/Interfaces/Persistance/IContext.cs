using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace CourseConstructors.CourseConstructors.Core.Interfaces.Persistance;

public interface IContext
{ 
    DbSet<Course> Courses { get; set; }
}