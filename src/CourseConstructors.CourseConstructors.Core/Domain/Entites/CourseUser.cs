using CourseConstructors.CourseConstructors.Core.Interfaces.Entities;

namespace CourseConstructors.CourseConstructors.Core.Domain.Entites;

public class CourseUser : BaseEntity
{
    public CourseUser()
    {
    }
    public CourseUser(Guid courseId, Guid userId)
    {
        CourseId = courseId;
        UserId = userId;
    }
    public Guid CourseId { get; set; }
    public virtual Course Course { get; set; } = null!;
    public Guid UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime EditDate { get; set; }
    public bool IsDeleted { get; set; }
}