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
    public async Task<Course> UpdateCourseAsync(Guid courseId, string courseName, string description, decimal cost, decimal saleCost, bool isSale, string imageUrl, bool isDelete)
    {
        var course = await context.Courses.FindAsync(courseId);
        if (course == null) throw new ArgumentException("Курс не найден");

        course.CourseName = courseName;
        course.Description = description;
        course.Cost = cost;
        course.SaleCost = saleCost;
        course.IsSale = isSale;
        course.ImageUrl = imageUrl;
        course.EditDate = DateTime.UtcNow;
        course.IsDeleted = isDelete;

        await context.SaveChangesAsync();
        return course;
    }

    public async Task<Lesson> AddLessonToCourse(Guid courseId, string title)
    {
        var lesson = new Lesson
        {
            CourseId = courseId,
            Title = title,
        };
        context.Lessons.Add(lesson);
        await context.SaveChangesAsync();
        return lesson;
    }

    public async Task<Lesson> UpdateLesson(Guid lessonId, string title, bool isDelete)
    {
        var lesson = await context.Lessons.FindAsync(lessonId);
        if (lesson == null) throw new ArgumentException("Урок не найден");

        lesson.Title = title;
        lesson.IsDeleted = isDelete;

        await context.SaveChangesAsync();
        return lesson;
    }

    public async Task<ContentItem> AddContentToLesson(Guid lessonId, string contentText, string imageUrl, int order)
    {
        var contentItem = new ContentItem
        {
            LessonId = lessonId,
            ContentText = contentText,
            ImageUrl = imageUrl,
            Order = order
        };
        context.ContentItems.Add(contentItem);
        await context.SaveChangesAsync();
        return contentItem;
    }

    public async Task<ContentItem> UpdateContentItem(Guid contentItemId, string contentText, string imageUrl, int order, bool isDeleted)
    {
        var contentItem = await context.ContentItems.FindAsync(contentItemId);
        if (contentItem == null) throw new ArgumentException("Содержимое урока не найдено");

        contentItem.ContentText = contentText;
        contentItem.ImageUrl = imageUrl;
        contentItem.Order = order;
        contentItem.IsDeleted = isDeleted;

        await context.SaveChangesAsync();
        return contentItem;
    }
    public async Task<List<Lesson>> GetAllLessons(Guid courseId)
    {
        return await context.Lessons
            .Include(l => l.ContentItems) 
            .Where(l => !l.IsDeleted &&
                        l.CourseId.Equals(courseId))
            .ToListAsync();
    }
    
    public async Task<List<ContentItem>> GetAllContentItemsByLesson(Guid lessonId)
    {
        return await context.ContentItems
            .Where(ci => ci.LessonId == lessonId)
            .OrderBy(ci => ci.Order)
            .ToListAsync();
    }


}