using CourseConstructors.CourseConstructors.Core;
using CourseConstructors.CourseConstructors.Core.Common.SharedResponses;
using CourseConstructors.CourseConstructors.Core.Interfaces.Services;
using CourseConstructors.CourseConstructors.Core.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace CourseConstructors.CourseConstructors.API.Controllers.v1;
/// <summary>
/// CRUD контроллер для курсов
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/course")]
[ApiController]
public class CourseController : BaseController
{
    private readonly IDistributedCacheService _distributedCacheService;
    private readonly IStringLocalizer<SharedResource> _localizer;
    private readonly TestOptions _testOptions;
    /// <summary>
    /// Параметризированный конструктор для контроллера
    /// </summary>
    /// <param name="localizer"></param>
    /// <param name="distributedCacheService"></param>
    /// <param name="testOptions"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public CourseController(
        IStringLocalizer<SharedResource> localizer,
        IDistributedCacheService distributedCacheService,
        IOptions<TestOptions> testOptions)
    {
        _distributedCacheService = distributedCacheService ?? throw new ArgumentNullException(nameof(distributedCacheService));
        _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
        _testOptions = testOptions.Value ?? throw new ArgumentNullException(nameof(localizer));
    }

    /// <summary>
    /// Тестовый метод
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Успешный ответ</response>
    /// <response code="400">Ошибка запроса</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [Route("test-route")]
    [HttpGet]
    [ProducesResponseType(typeof(List<string>), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(ErrorResponse), 500)]
    public async Task<IActionResult> TestRoute()
    {
        var t = await Task.Run(() => 5 + 5);
        // throw new Exception("testik");
        return Ok(t);
    }
    
    /// <summary>
    /// Тестовый redis
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Успешный ответ</response>
    /// <response code="400">Ошибка запроса</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [Route("countries")]
    [HttpGet]
    [ProducesResponseType(typeof(List<string>), 200)]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(ErrorResponse), 500)]
    public async Task<IActionResult> Countries()
    {
        var cachedCountry = await _distributedCacheService.GetString("country");
        if(!string.IsNullOrEmpty(cachedCountry))
            return Ok(new{cachedData = cachedCountry});
        const string answer = "serializers";
        await _distributedCacheService.SetString("country", answer, TimeSpan.FromMinutes(5));
        return Ok(answer);
    }
    
    /// <summary>
    /// Эндпоинт для очистки кэша
    /// </summary>
    /// <returns></returns>
    [HttpGet("cached/remove")]
    public async Task<IActionResult> ClearCachedCountry()
    {
        await _distributedCacheService.RemoveString("country");
        return Ok("Removed");
    }
    
    /// <summary>
    /// Проверка языка
    /// </summary>
    /// <returns></returns>
    [HttpGet("test-lang")]
    public IActionResult TestLang()
    {
        return Ok(string.Join("", _localizer["тест"].Value.Concat(_testOptions.TestOpt)));
    }
}