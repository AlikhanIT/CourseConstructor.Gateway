using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CourseConstructors.CourseConstructors.Core.Interfaces.Entities;

namespace CourseConstructors.CourseConstructors.Core.Domain.Entites;

public class ContentItem : BaseEntity
{
    [Key]
    public Guid ContentItemId { get; set; }

    public Guid LessonId { get; set; }

    public string ContentText { get; set; } = string.Empty; 

    public string ImageUrl { get; set; } = string.Empty; 

    public int Order { get; set; }

    public virtual Lesson Lesson { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime EditDate { get; set; }
    public bool IsDeleted { get; set; }
}
