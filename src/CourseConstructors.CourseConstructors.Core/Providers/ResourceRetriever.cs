using CourseConstructors.CourseConstructors.Core.Resources;
using CourseConstructors.CourseConstructors.Core.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace CourseConstructors.CourseConstructors.Core.Interfaces.Providers;
public class ResourceRetriever : IResourceRetriever
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly ILogger<ResourceRetriever> _logger;

    public ResourceRetriever(IStringLocalizer<SharedResource> localizer,
        ILogger<ResourceRetriever> logger)
    {
        _localizer = localizer;
        _logger = logger;
    }

    public string GetResource(string key)
    {
        _logger.LogInformation("Достаем из ресурса локализованную строку по ключу {key}", key);
        return _localizer[key];
    }
}