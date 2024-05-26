using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using MediatR;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class UpdateContentItemCommand : IRequest<Result<ContentItem>>
{
    public UpdateContentItemCommand(Guid contentItemId, string contentText, string imageUrl, int order, bool isDeleted)
    {
        ContentItemId = contentItemId;
        ContentText = contentText;
        ImageUrl = imageUrl;
        Order = order;
        IsDeleted = isDeleted;
    }
    public Guid ContentItemId { get; set; }
    public string ContentText { get; set; }
    public string ImageUrl { get; set; }
    public int Order { get; set; }
    public bool IsDeleted { get; set; }
}