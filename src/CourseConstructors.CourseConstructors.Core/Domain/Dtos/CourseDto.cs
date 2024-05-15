namespace CourseConstructors.CourseConstructors.Core.Domain.Dtos;

public class CourseDto
{
    public Guid CourseId { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}