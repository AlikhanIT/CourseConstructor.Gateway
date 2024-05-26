using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using MediatR;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class AddLessonToCourseCommand : IRequest<Result<Lesson>>
{
    public AddLessonToCourseCommand(Guid courseId, string title)
    {
        CourseId = courseId;
        Title = title;
    }
    public Guid CourseId { get; set; }
    public string Title { get; set; }
}