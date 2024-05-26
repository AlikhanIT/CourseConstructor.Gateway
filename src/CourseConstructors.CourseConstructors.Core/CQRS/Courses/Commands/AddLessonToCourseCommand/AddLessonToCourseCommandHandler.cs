using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using CourseConstructors.CourseConstructors.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class AddLessonToCourseCommandHandler : IRequestHandler<AddLessonToCourseCommand, Result<Lesson>>
{
    private readonly ICourseRepositoryService _courseRepositoryService;
    private readonly ILogger<AddLessonToCourseCommandHandler> _logger;
    public AddLessonToCourseCommandHandler(ICourseRepositoryService courseRepositoryService,
        ILogger<AddLessonToCourseCommandHandler> logger)
    {
        _courseRepositoryService = courseRepositoryService ?? throw new ArgumentNullException(nameof(courseRepositoryService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Result<Lesson>> Handle(AddLessonToCourseCommand request, CancellationToken cancellationToken)
    {
        var result = await _courseRepositoryService.AddLessonToCourse(request.CourseId, request.Title);
        
        return result is not null ? new Result<Lesson>(result) : Result<Lesson>.Error("Не удалось добавить уроки");
    }
}