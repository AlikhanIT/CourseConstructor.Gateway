using System.Linq.Expressions;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using CourseConstructors.CourseConstructors.Core.Interfaces.Services;
using CourseConstructors.CourseConstructors.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace CourseConstructors.CourseConstructors.Infrastructure.Services;

public class CourseRepositoryService(Context context) : ICourseRepositoryService
{
    public async Task<bool> AddCourse(Course course)
    {
        try
        {
            await context.Courses.AddAsync(course);

            return await context.SaveChangesAsync() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        await context.Courses.AddAsync(course);

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateCourseDescription(Course course)
    {
        context.Courses.Attach(course);
        context.Entry(course)
            .Property(x => x.Description).IsModified = true;

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateCourse(Course course)
    {
        context.Courses.Update(course);

        return await context.SaveChangesAsync() > 0;
    }


    public async Task<bool> DeleteCourse(Course course)
    {
        course.Delete();
        context.Courses.Attach(course);
        context.Entry(course)
            .Property(x => x.IsDeleted).IsModified = true;

        return await context.SaveChangesAsync() > 0;
    }

    public async Task<Course> GetCourseById(Guid id)
    {
        var course = await context.Courses.FirstOrDefaultAsync(f => f.CourseId.Equals(id));
        return course ?? new Course();
    }

    public async Task<List<Course>> GetCoursesByConditionList(Expression<Func<Course?, bool>> condition)
    {
        return (await context.Courses.Where(condition).ToListAsync())!;
    }
    
    public async Task<bool> AddCourseToUser(CourseUser courseUser)
    {
        await context.CourseToUsers.AddAsync(courseUser);

        return await context.SaveChangesAsync() > 0;
    }
    
    public async Task<List<Course>> GetUserCourses(Guid userId)
    {
        return (await context.Courses
            .Where(course => course.Users.Any(user => user.UserId == userId))
            .ToListAsync())!;
    }
}