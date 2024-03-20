using System.ComponentModel.DataAnnotations;
using CourseConstructors.CourseConstructors.Core.Interfaces.Entities;

namespace CourseConstructors.CourseConstructors.Core.Domain.Entites;

public class Course : BaseEntity
{
    [Key]
    public int CourseId { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Cost { get; set; }
    public decimal SaleCost { get; set; }
    public bool IsSale { get; set; } = false;
    public DateTime CreatedDate { get; set; }
    public DateTime EditDate { get; set; }
    public bool IsDeleted { get; set; }
    public List<CourseUser> Users { get; set; } = null!;

    public Course DeleteCourse()
    {
        IsDeleted = true;

        return this;
    }
}