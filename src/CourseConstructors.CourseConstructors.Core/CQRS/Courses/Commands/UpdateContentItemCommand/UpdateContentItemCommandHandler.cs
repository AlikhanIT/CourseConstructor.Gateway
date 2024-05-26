using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using CourseConstructors.CourseConstructors.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class UpdateContentItemCommandHandler : IRequestHandler<UpdateContentItemCommand, Result<ContentItem>>
{
    private readonly ICourseRepositoryService _courseRepositoryService;
    private readonly ILogger<UpdateContentItemCommandHandler> _logger;
    public UpdateContentItemCommandHandler(ICourseRepositoryService courseRepositoryService,
        ILogger<UpdateContentItemCommandHandler> logger)
    {
        _courseRepositoryService = courseRepositoryService ?? throw new ArgumentNullException(nameof(courseRepositoryService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Result<ContentItem>> Handle(UpdateContentItemCommand request, CancellationToken cancellationToken)
    {
        var result = await _courseRepositoryService.UpdateContentItem(request.ContentItemId, request.ContentText, request.ImageUrl, request.Order, request.IsDeleted);
        
        return result is not null ? new Result<ContentItem>(result) : Result<ContentItem>.Error("Не удалось изменить контент в уроке");
    }
}