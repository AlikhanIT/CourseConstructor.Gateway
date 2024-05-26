using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace CourseConstructors.CourseConstructors.Core.Interfaces.Persistance;

public interface IContext
{ 
    DbSet<Course> Courses { get; set; }
    DbSet<CourseUser> CourseToUsers { get; set; }
    DbSet<Lesson> Lessons { get; set; }
    DbSet<ContentItem> ContentItems { get; set; }

}