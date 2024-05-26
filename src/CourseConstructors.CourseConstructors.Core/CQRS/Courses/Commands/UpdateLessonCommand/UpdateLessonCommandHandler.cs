using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using CourseConstructors.CourseConstructors.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand, Result<Lesson>>
{
    private readonly ICourseRepositoryService _courseRepositoryService;
    private readonly ILogger<UpdateLessonCommandHandler> _logger;
    public UpdateLessonCommandHandler(ICourseRepositoryService courseRepositoryService,
        ILogger<UpdateLessonCommandHandler> logger)
    {
        _courseRepositoryService = courseRepositoryService ?? throw new ArgumentNullException(nameof(courseRepositoryService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Result<Lesson>> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        var result = await _courseRepositoryService.UpdateLesson(request.LessonId, request.Title, request.IsDelete);
        
        return result is not null ? new Result<Lesson>(result) : Result<Lesson>.Error("Не удалось обновить уроки");
    }
}