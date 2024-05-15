using System.Linq.Expressions;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;

namespace CourseConstructors.CourseConstructors.Core.Interfaces.Services;

public interface ICourseRepositoryService
{
    Task<bool> AddCourse(Course course);
    Task<bool> UpdateCourseDescription(Course course);
    Task<bool> UpdateCourse(Course course);
    Task<bool> DeleteCourse(Course course);
    Task<Course> GetCourseById(Guid id);
    Task<List<Course>> GetCoursesByConditionList(Expression<Func<Course?, bool>> condition);
    Task<bool> AddCourseToUser(CourseUser courseUser);
    Task<List<Course>> GetUserCourses(Guid userId);
}