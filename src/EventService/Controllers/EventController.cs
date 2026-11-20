using Microsoft.AspNetCore.Mvc;

namespace Sprint1;

/// <summary>
/// Контроллер для работы с событиями (Event)
/// </summary>
/// <param name="_eventsService"></param>
[ApiController]
[Route("api/[controller]")]
public class EventsController(IEventService _eventsService) : ControllerBase
{
    /// <summary>
    /// Получить все события
    /// </summary>
    /// <param name="title"></param>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<List<Event>> GetAllEvents(
        [FromQuery] string? title,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        return Ok(_eventsService.GetAllEvents(title, from, to, page, pageSize));
    }

    /// <summary>
    /// Метод возвращает событие по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [HttpGet("{id:int}")]
    public ActionResult<Event> GetEvent(int id)
    {
        return Ok(_eventsService.GetEventById(id));
    }

    /// <summary>
    /// Создать событие
    /// </summary>
    /// <param name="eventDto"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Create([FromBody] EventDto eventDto)
    {
        return StatusCode(StatusCodes.Status201Created, _eventsService.CreateEvent(eventDto));
    }

    /// <summary>
    /// Изменить событие по ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="eventDto"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] EventDto eventDto)
    {
        return Ok(_eventsService.UpdateEvent(id, eventDto));
    }

    /// <summary>
    /// Удалить событие по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        _eventsService.DeleteEvent(id);
        return NoContent();
    }
}