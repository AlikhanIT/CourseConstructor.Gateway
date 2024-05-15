using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using CourseConstructors.CourseConstructors.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Result<Course>>
{
    private readonly ICourseRepositoryService _courseRepositoryService;
    private readonly ILogger<CreateCourseCommandHandler> _logger;
    public CreateCourseCommandHandler(ICourseRepositoryService courseRepositoryService,
        ILogger<CreateCourseCommandHandler> logger)
    {
        _courseRepositoryService = courseRepositoryService ?? throw new ArgumentNullException(nameof(courseRepositoryService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Result<Course>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var newCourse = new Course(request.CourseName);
        var isCreated = await _courseRepositoryService.AddCourse(newCourse);
        
        return isCreated ? new Result<Course>(newCourse) : Result<Course>.Error();
    }
}