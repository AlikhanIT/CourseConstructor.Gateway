using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using CourseConstructors.CourseConstructors.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class GetAllLessonsQueryHandler : IRequestHandler<GetAllLessonsQuery, Result<List<Lesson>>>
{
    private readonly ICourseRepositoryService _courseRepositoryService;
    private readonly ILogger<GetAllLessonsQueryHandler> _logger;

    public GetAllLessonsQueryHandler(ICourseRepositoryService courseRepositoryService,
            ILogger<GetAllLessonsQueryHandler> logger
        )
    {
        _courseRepositoryService = courseRepositoryService ?? throw new ArgumentNullException(nameof(courseRepositoryService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Result<List<Lesson>>> Handle(GetAllLessonsQuery request, CancellationToken cancellationToken)
    {
        var result = await _courseRepositoryService.GetAllLessons(request.CourseId);
        
        return result is null ? Result<List<Lesson>>.NotFound() : new Result<List<Lesson>>(result);
    }
}