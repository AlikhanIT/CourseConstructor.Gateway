namespace CourseConstructors.CourseConstructors.Core.Common.SharedResponses;

public class Response<T>
{
    public bool IsSuccess { get; set; } = false;
    public bool IsFailed { get; set; } = false;
    public string Error { get; set; } = string.Empty;
    public T? Value { get; set; }
    
    
}