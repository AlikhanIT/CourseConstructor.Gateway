using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using MediatR;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class GetCourseByIdQuery : IRequest<Result<Course>>
{
    public GetCourseByIdQuery(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; set; }
}