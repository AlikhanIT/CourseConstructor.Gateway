using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using CourseConstructors.CourseConstructors.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class GetAllUserCoursesQueryHandler : IRequestHandler<GetAllUserCoursesQuery, Result<List<Course>>>
{
    private readonly ICourseRepositoryService _courseRepositoryService;
    private readonly ILogger<GetAllUserCoursesQueryHandler> _logger;

    public GetAllUserCoursesQueryHandler(ICourseRepositoryService courseRepositoryService,
            ILogger<GetAllUserCoursesQueryHandler> logger
        )
    {
        _courseRepositoryService = courseRepositoryService ?? throw new ArgumentNullException(nameof(courseRepositoryService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Result<List<Course>>> Handle(GetAllUserCoursesQuery request, CancellationToken cancellationToken)
    {
        var courseToUsers = await _courseRepositoryService.GetUserCourses(request.UserId);
        
        return courseToUsers.Count.Equals(0) ? Result<List<Course>>.NotFound() : new Result<List<Course>>(courseToUsers);
    }
}