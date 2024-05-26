using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using MediatR;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class AddContentToLessonCommand : IRequest<Result<ContentItem>>
{
    public AddContentToLessonCommand(Guid lessonId, string contentText, string imageUrl, int order)
    {
        LessonId = lessonId;
        ContentText = contentText;
        ImageUrl = imageUrl;
        Order = order;
    }
    public Guid LessonId { get; set; }
    public string ContentText { get; set; }
    public string ImageUrl { get; set; }
    public int Order { get; set; }
}