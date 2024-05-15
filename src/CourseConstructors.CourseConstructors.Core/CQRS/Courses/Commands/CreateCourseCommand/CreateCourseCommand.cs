using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using MediatR;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class CreateCourseCommand : IRequest<Result<Course>>
{
    public CreateCourseCommand(string courseName)
    {
        CourseName = courseName;
    }
    public string CourseName { get; set; }
}