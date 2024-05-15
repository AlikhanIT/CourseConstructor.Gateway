using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using MediatR;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class AddCourseToUserCommand : IRequest<Result<CourseUser>>
{
    public AddCourseToUserCommand(Guid courseId, Guid userId)
    {
        CourseId = courseId;
        UserId = userId;
    }
    public Guid CourseId { get; set; }
    public Guid UserId { get; set; }
}