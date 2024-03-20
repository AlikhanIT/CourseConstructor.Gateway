namespace CourseConstructors.CourseConstructors.Core.Interfaces.Entities;

public interface BaseEntity
{
    DateTime CreatedDate { get; set; }
    DateTime EditDate { get; set; }
    bool IsDeleted { get; set; }
}