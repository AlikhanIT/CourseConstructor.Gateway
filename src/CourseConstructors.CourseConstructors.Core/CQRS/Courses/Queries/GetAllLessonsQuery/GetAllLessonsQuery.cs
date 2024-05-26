using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using MediatR;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class GetAllLessonsQuery : IRequest<Result<List<Lesson>>>
{
    public GetAllLessonsQuery()
    {
    }

    public GetAllLessonsQuery(Guid courseId)
    {
        CourseId = courseId;
    }

    public Guid CourseId { get; set; }
}