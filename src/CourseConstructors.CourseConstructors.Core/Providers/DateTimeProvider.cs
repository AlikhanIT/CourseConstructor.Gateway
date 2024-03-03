namespace CourseConstructors.CourseConstructors.Core.Interfaces.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetCurrentTime() => DateTime.UtcNow.AddHours(5);
}