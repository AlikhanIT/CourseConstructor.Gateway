using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using CourseConstructors.CourseConstructors.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, Result<Course>>
{
    private readonly ICourseRepositoryService _courseRepositoryService;
    private readonly ILogger<GetCourseByIdQueryHandler> _logger;

    public GetCourseByIdQueryHandler(ICourseRepositoryService courseRepositoryService,
            ILogger<GetCourseByIdQueryHandler> logger
        )
    {
        _courseRepositoryService = courseRepositoryService ?? throw new ArgumentNullException(nameof(courseRepositoryService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Result<Course>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepositoryService.GetCourseById(request.Id);
        
        return new Result<Course>(course);
    }
}