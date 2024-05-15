using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using MediatR;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class GetAllUserCoursesQuery : IRequest<Result<List<Course>>>
{
    public GetAllUserCoursesQuery(Guid userId)
    {
        UserId = userId;
    }
    public Guid UserId { get; set; }
}