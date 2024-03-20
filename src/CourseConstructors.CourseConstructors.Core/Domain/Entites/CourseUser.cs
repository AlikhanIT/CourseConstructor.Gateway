using CourseConstructors.CourseConstructors.Core.Interfaces.Entities;

namespace CourseConstructors.CourseConstructors.Core.Domain.Entites;

public class CourseUser : BaseEntity
{
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime EditDate { get; set; }
    public bool IsDeleted { get; set; }
}