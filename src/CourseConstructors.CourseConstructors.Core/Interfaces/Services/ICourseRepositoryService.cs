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
    Task<Course> UpdateCourseAsync(Guid courseId, string courseName, string description, decimal cost, decimal saleCost, bool isSale, string imageUrl, bool isDelete);
    Task<Lesson> AddLessonToCourse(Guid courseId, string title);
    Task<Lesson> UpdateLesson(Guid lessonId, string title, bool isDelete);
    Task<ContentItem> AddContentToLesson(Guid lessonId, string contentText, string imageUrl, int order);
    Task<ContentItem> UpdateContentItem(Guid contentItemId, string contentText, string imageUrl, int order,
        bool isDeleted);

    Task<List<ContentItem>> GetAllContentItemsByLesson(Guid lessonId);
    Task<List<Lesson>> GetAllLessons(Guid courseId);

}