using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using CourseConstructors.CourseConstructors.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, Result<List<Course>>>
{
    private readonly ICourseRepositoryService _courseRepositoryService;
    private readonly ILogger<CreateCourseCommandHandler> _logger;

    public GetAllCoursesQueryHandler(ICourseRepositoryService courseRepositoryService,
            ILogger<CreateCourseCommandHandler> logger
        )
    {
        _courseRepositoryService = courseRepositoryService ?? throw new ArgumentNullException(nameof(courseRepositoryService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Result<List<Course>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _courseRepositoryService.GetCoursesByConditionList(x => true);
        
        return courses.Count.Equals(0) ? Result<List<Course>>.NotFound() : new Result<List<Course>>(courses);
    }
}