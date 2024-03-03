using CourseConstructors.CourseConstructors.Core.Common.SharedResponses;
using Microsoft.AspNetCore.Mvc;

namespace CourseConstructors.CourseConstructors.API.Controllers;
/// <summary>
/// CRUD контроллер для курсов
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/course")]
[ApiController]
public class CourseController : BaseController
{
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
}