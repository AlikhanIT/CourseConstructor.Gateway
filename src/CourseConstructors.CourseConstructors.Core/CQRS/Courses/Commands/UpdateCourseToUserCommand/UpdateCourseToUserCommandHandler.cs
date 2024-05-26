using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using CourseConstructors.CourseConstructors.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class UpdateCourseToUserCommandHandler : IRequestHandler<UpdateCourseToUserCommand, Result<Course>>
{
    private readonly ICourseRepositoryService _courseRepositoryService;
    private readonly ILogger<UpdateCourseToUserCommandHandler> _logger;
    public UpdateCourseToUserCommandHandler(ICourseRepositoryService courseRepositoryService,
        ILogger<UpdateCourseToUserCommandHandler> logger)
    {
        _courseRepositoryService = courseRepositoryService ?? throw new ArgumentNullException(nameof(courseRepositoryService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Result<Course>> Handle(UpdateCourseToUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _courseRepositoryService.UpdateCourseAsync(request.CourseId, request.CourseName, request.Description,request.Cost, request.SaleCost, request.IsSale, request.ImageUrl, request.IsDelete);
        
        return result != null ? new Result<Course>(result) : Result<Course>.Error("Не удалось изменить курс");
    }
}