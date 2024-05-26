using Ardalis.Result;
using CourseConstructors.CourseConstructors.Core.Domain.Entites;
using MediatR;

namespace CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;

public class UpdateCourseToUserCommand : IRequest<Result<Course>>
{
    public UpdateCourseToUserCommand(Guid courseId, string courseName, string description, decimal cost, decimal saleCost, bool isSale, string imageUrl, bool isDelete)
    {
        CourseId = courseId;
        CourseName = courseName;
        Description = description;
        Cost = cost;
        SaleCost = saleCost;
        IsSale = isSale;
        ImageUrl = imageUrl;
        IsDelete = isDelete;
    }
    public Guid CourseId { get; set; }
    public string CourseName { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public decimal SaleCost { get; set; }
    public bool IsSale { get; set; }
    public string ImageUrl { get; set; }
    public bool IsDelete { get; set; }
}