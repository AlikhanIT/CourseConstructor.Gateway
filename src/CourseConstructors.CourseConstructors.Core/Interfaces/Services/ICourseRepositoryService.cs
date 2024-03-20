using System.Linq.Expressions;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;

namespace CourseConstructors.CourseConstructors.Core.Interfaces.Services;

public interface ICourseRepositoryService
{
    Task<bool> AddCourse(Course course);
    Task<bool> UpdateCourseDescription(Course course);
    Task<bool> UpdateCourse(Course course);
    Task<bool> DeleteCourse(Course course);
    Task<Course> GetCourseByCondition(Expression<Func<Course, bool>> condition);
    Task<List<Course?>> GetCoursesByCondition(Expression<Func<Course?, bool>> condition);
}