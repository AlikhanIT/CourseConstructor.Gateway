using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using MediatR;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class GetAllContentItemsByLessonQuery : IRequest<Result<List<ContentItem>>>
{
    public GetAllContentItemsByLessonQuery() { }

    public GetAllContentItemsByLessonQuery(Guid lessonId)
    {
        LessonId = lessonId;
    }

    public Guid LessonId { get; set; }
}