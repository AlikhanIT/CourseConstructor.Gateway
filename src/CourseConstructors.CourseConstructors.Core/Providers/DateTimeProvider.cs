using CourseConstructors.CourseConstructors.Core.Interfaces.Providers;

namespace CourseConstructors.CourseConstructors.Core.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetCurrentTime() => DateTime.UtcNow.AddHours(5);
}