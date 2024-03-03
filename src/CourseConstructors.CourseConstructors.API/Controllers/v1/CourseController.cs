using CourseConstructors.CourseConstructors.Core.Common.SharedResponses;
using CourseConstructors.CourseConstructors.Core.Resources;
using CourseConstructors.CourseConstructors.Core.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace CourseConstructors.CourseConstructors.API.Controllers;
/// <summary>
/// CRUD контроллер для курсов
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/course")]
[ApiController]
public class CourseController : BaseController
{
    private readonly IDistributedCache _distributedCache;
    private readonly IStringLocalizer<SharedResource> _localizer;
    public CourseController(IDistributedCache distributedCache, IStringLocalizer<SharedResource> localizer)
    {
        _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        _localizer = localizer  ?? throw new ArgumentNullException(nameof(localizer));
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
        throw new Exception("testik");
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
        string cachedCountry = await _distributedCache.GetStringAsync("country");
        if(!string.IsNullOrEmpty(cachedCountry))
        {
            return Ok(new{cachedData = cachedCountry});
        }
        var answer = "serializesssss";
        await _distributedCache.SetStringAsync("country", answer);
        return Ok(answer);
    }
    [HttpGet("cached/remove")]
    public async Task<IActionResult> ClearCachedCountry()
    {
        await _distributedCache.RemoveAsync("country");
        return Ok("Removed");
    }
    
    [HttpGet("test-lang")]
    public async Task<IActionResult> TestLang()
    {
        return Ok(_localizer["тест"]);
    }
}