using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using CourseConstructors.CourseConstructors.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class AddCourseToUserCommandHandler : IRequestHandler<AddCourseToUserCommand, Result<CourseUser>>
{
    private readonly ICourseRepositoryService _courseRepositoryService;
    private readonly ILogger<AddCourseToUserCommandHandler> _logger;
    public AddCourseToUserCommandHandler(ICourseRepositoryService courseRepositoryService,
        ILogger<AddCourseToUserCommandHandler> logger)
    {
        _courseRepositoryService = courseRepositoryService ?? throw new ArgumentNullException(nameof(courseRepositoryService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Result<CourseUser>> Handle(AddCourseToUserCommand request, CancellationToken cancellationToken)
    {
        var courseToUser = new CourseUser(request.CourseId, request.UserId);
        var isCreated = await _courseRepositoryService.AddCourseToUser(courseToUser);
        
        return isCreated ? new Result<CourseUser>(courseToUser) : Result<CourseUser>.Error("test");
    }
}