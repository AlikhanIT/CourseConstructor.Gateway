using CourseConstructors.CourseConstructors.Core;
using CourseConstructors.CourseConstructors.Core.Common.SharedResponses;
using CourseConstructors.CourseConstructors.Core.CQRS.Courses.Commands;
using CourseConstructors.CourseConstructors.Core.Interfaces.Services;
using CourseConstructors.CourseConstructors.Core.Options;
using MediatR;
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
    private readonly ILogger<CourseController> _logger;
    private readonly ISender _sender;

    /// <summary>
    /// Параметризированный конструктор для контроллера
    /// </summary>
    /// <param name="localizer"></param>
    /// <param name="distributedCacheService"></param>
    /// <param name="testOptions"></param>
    /// <param name="logger"></param>
    /// <param name="sender"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public CourseController(
        IStringLocalizer<SharedResource> localizer,
        IDistributedCacheService distributedCacheService,
        IOptions<TestOptions> testOptions,
        ILogger<CourseController> logger,
        ISender sender)
    {
        _distributedCacheService =
            distributedCacheService ?? throw new ArgumentNullException(nameof(distributedCacheService));
        _logger = logger;
        _sender = sender;
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
        if (!string.IsNullOrEmpty(cachedCountry))
            return Ok(new { cachedData = cachedCountry });
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

    /// <summary>
    /// Создание курса
    /// </summary>
    /// <returns></returns>
    [HttpPost("create-course/{courseName}")]
    public async Task<IActionResult> CreateCourse(string courseName)
    {
        _logger.LogInformation("Создание курса");
        var res = await _sender.Send(new CreateCourseCommand(courseName));
        return Ok(res.Value);
    }

    /// <summary>
    /// Получение всех курсов
    /// </summary>
    /// <returns></returns>
    [HttpPost("all")]
    public async Task<IActionResult> GetAllCourses()
    {
        _logger.LogInformation("Получение всех курсов");
        var res = await _sender.Send(new GetAllCoursesQuery());
        return Ok(res.Value);
    }

    /// <summary>
    /// Добавить курс к пользователю
    /// </summary>
    /// <returns></returns>
    [HttpPost("add-to-user/{userId}/{courseId}")]
    public async Task<IActionResult> GetAllCourses(Guid userId, Guid courseId)
    {
        _logger.LogInformation("Добавление курса к пользователю");
        var res = await _sender.Send(new AddCourseToUserCommand(courseId, userId));
        return Ok(res.Value);
    }


    /// <summary>
    /// Получить все курсы по пользовавтелю
    /// </summary>
    /// <returns></returns>
    [HttpGet("of-user/{userId}")]
    public async Task<IActionResult> GetAllCourses(Guid userId)
    {
        _logger.LogInformation("Получить все курсы по пользовавтелю");
        var res = await _sender.Send(new GetAllUserCoursesQuery(userId));
        return Ok(res.Value);
    }

    /// <summary>
    /// Получить курс по айди
    /// </summary>
    /// <returns></returns>
    [HttpGet("by-id/{id}")]
    public async Task<IActionResult> GetCourseById(Guid id)
    {
        _logger.LogInformation("Получить курс по айди {id}", id);
        var res = await _sender.Send(new GetCourseByIdQuery(id));
        return Ok(res.Value);
    }

    /// <summary>
    /// Обновляет данные курса в системе.
    /// </summary>
    [HttpPost("updateCourse")]
    public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseToUserCommand command)
    {
        _logger.LogInformation("Updating course {CourseId}", command.CourseId);
        var result = await _sender.Send(command);
        return Ok(result.Value);
    }

    /// <summary>
    /// Добавляет новый урок в указанный курс.
    /// </summary>
    [HttpPost("addLesson")]
    public async Task<IActionResult> AddLesson([FromBody] AddLessonToCourseCommand command)
    {
        _logger.LogInformation("Adding lesson to course {CourseId}", command.CourseId);
        var result = await _sender.Send(command);
        return Ok(result.Value);
    }

    /// <summary>
    /// Обновляет данные урока, включая возможность его удаления.
    /// </summary>
    [HttpPost("updateLesson")]
    public async Task<IActionResult> UpdateLesson([FromBody] UpdateLessonCommand command)
    {
        _logger.LogInformation("Updating lesson {CourseId}", command.LessonId);
        var result = await _sender.Send(command);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получает все элементы контента для указанного урока.
    /// </summary>
    [HttpGet("getAllContentItems")]
    public async Task<IActionResult> GetAllContentItems([FromQuery] GetAllContentItemsByLessonQuery query)
    {
        _logger.LogInformation("Getting all content items for lesson {LessonId}", query.LessonId);
        var result = await _sender.Send(query);
        return Ok(result.Value);
    }

    /// <summary>
    /// Добавляет контент в урок.
    /// </summary>
    [HttpPost("addContentToLesson")]
    public async Task<IActionResult> AddContentToLesson([FromBody] AddContentToLessonCommand command)
    {
        _logger.LogInformation("Adding content to lesson {LessonId}", command.LessonId);
        var result = await _sender.Send(command);
        return Ok(result.Value);
    }

    /// <summary>
    /// Получает список всех уроков для указанного курса.
    /// </summary>
    [HttpGet("getAllLessons")]
    public async Task<IActionResult> GetAllLessons([FromQuery] GetAllLessonsQuery query)
    {
        _logger.LogInformation("Getting all lessons for course {CourseId}", query.CourseId);
        var result = await _sender.Send(query);
        return Ok(result.Value);
    }

    /// <summary>
    /// Обновляет элемент контента в уроке.
    /// </summary>
    [HttpPost("updateContentItem")]
    public async Task<IActionResult> UpdateContentItem([FromBody] UpdateContentItemCommand command)
    {
        _logger.LogInformation("Updating content item {ContentItemId}", command.ContentItemId);
        var result = await _sender.Send(command);
        return Ok(result.Value);
    }
}