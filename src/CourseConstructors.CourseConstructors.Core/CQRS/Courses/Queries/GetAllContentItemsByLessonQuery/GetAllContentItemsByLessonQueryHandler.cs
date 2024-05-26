using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using CourseConstructors.CourseConstructors.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class GetAllContentItemsByLessonQueryHandler : IRequestHandler<GetAllContentItemsByLessonQuery, Result<List<ContentItem>>>
{
    private readonly ICourseRepositoryService _courseRepositoryService;
    private readonly ILogger<GetAllContentItemsByLessonQueryHandler> _logger;

    public GetAllContentItemsByLessonQueryHandler(ICourseRepositoryService courseRepositoryService,
            ILogger<GetAllContentItemsByLessonQueryHandler> logger
        )
    {
        _courseRepositoryService = courseRepositoryService ?? throw new ArgumentNullException(nameof(courseRepositoryService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Result<List<ContentItem>>> Handle(GetAllContentItemsByLessonQuery request, CancellationToken cancellationToken)
    {
        var result = await _courseRepositoryService.GetAllContentItemsByLesson(request.LessonId);
        
        return result is null ? Result<List<ContentItem>>.NotFound() : new Result<List<ContentItem>>(result);
    }
}