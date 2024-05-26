using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using CourseConstructors.CourseConstructors.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class AddContentToLessonCommandHandler : IRequestHandler<AddContentToLessonCommand, Result<ContentItem>>
{
    private readonly ICourseRepositoryService _courseRepositoryService;
    private readonly ILogger<AddContentToLessonCommandHandler> _logger;
    public AddContentToLessonCommandHandler(ICourseRepositoryService courseRepositoryService,
        ILogger<AddContentToLessonCommandHandler> logger)
    {
        _courseRepositoryService = courseRepositoryService ?? throw new ArgumentNullException(nameof(courseRepositoryService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Result<ContentItem>> Handle(AddContentToLessonCommand request, CancellationToken cancellationToken)
    {
        var result = await _courseRepositoryService.AddContentToLesson(request.LessonId, request.ContentText, request.ImageUrl, request.Order);
        
        return result is not null ? new Result<ContentItem>(result) : Result<ContentItem>.Error("Не удалось добавить контент в урок");
    }
}