using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using MediatR;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class UpdateLessonCommand : IRequest<Result<Lesson>>
{
    public UpdateLessonCommand(Guid lessonId, string title, bool isDelete)
    {
        LessonId = lessonId;
        Title = title;
        IsDelete = isDelete;
    }
    public Guid LessonId { get; set; }
    public string Title { get; set; }
    public bool IsDelete { get; set; }
}