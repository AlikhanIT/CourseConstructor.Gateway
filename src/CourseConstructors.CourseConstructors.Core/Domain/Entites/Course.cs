using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CourseConstructors.CourseConstructors.Core.Interfaces.Entities;

namespace CourseConstructors.CourseConstructors.Core.Domain.Entites;

public class Course : BaseEntity
{
    public Course()
    {
        
    }
    public Course(string courseName)
    {
        CourseName = courseName;
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public Guid CourseId { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Cost { get; set; } = 0;
    public decimal SaleCost { get; set; } = 0;
    public bool IsSale { get; set; } = false;
    public DateTime CreatedDate { get; set; }
    public DateTime EditDate { get; set; }
    public bool IsDeleted { get; set; } = false;
    public string ImageUrl { get; set; } = string.Empty;
    public virtual List<Lesson> Lessons { get; set; } 
    public virtual List<CourseUser> Users { get; set; } = null!;

    public Course Delete()
    {
        IsDeleted = true;

        return this;
    }
}