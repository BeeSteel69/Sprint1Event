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
    /// <returns></returns>
    [HttpGet]
    public ActionResult<List<Event>> GetAllEvents()
    {
        return _eventsService.GetAllEvents();
    }

    /// <summary>
    /// Метод возвращает номер здания по индексу из коллекции
    /// </summary>
    /// <param name="index">Параметр индекса, для получения номера здания</param>
    /// <response code="200">Возвращается JSON-структура ApiBaseResult с деталями ответа
    /// и HTTP статус-кодом 200 Ok в случае успеха</response>
    
    [Produces("application/json")]
    [HttpGet("{id:int}")]
    public ActionResult<Event> GetEvent(int id)
    {
        var eventItem=_eventsService.GetEventById(id);

        if (eventItem == null)
        {
            // Возвращаем 404
            return NotFound();
        }

        // Возвращаем 200
        return Ok(eventItem);
    }

    /// <summary>
    /// Создать событие
    /// </summary>
    /// <param name="eventDto"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Create([FromBody] EventDto eventDto)
    {
       
        if (eventDto.EndAt <= eventDto.StartAt)
        {
            ModelState.AddModelError("", "Дата окончания (EndAt) должна быть строго позже даты начала (StartAt).");
        }
        if (!ModelState.IsValid)
        {
            // Вернёт статус 400 Bad Request и JSON со списком ошибок
            return BadRequest(ModelState);
        }

        // Добавляем событие
        _eventsService.CreateEvent(new Event(eventDto.Title, eventDto.StartAt, eventDto.EndAt, description: eventDto.Description));

        //Возвращаем 201
        return new CreatedResult();
    }

    /// <summary>
    /// Изменить событие по ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="eventDto"></param>
    /// <returns></returns>
    [HttpPut("events/{id}")]
    public ActionResult Update(int id,[FromBody] EventDto eventDto)
    {
        var eventItem = _eventsService.GetEventById(id);

        if (eventItem == null)
        {
            // Возвращаем 404
            return NotFound();
        }

        if (eventDto.EndAt <= eventDto.StartAt)
        {
            ModelState.AddModelError("", "Дата окончания (EndAt) должна быть строго позже даты начала (StartAt).");
        }
        if (!ModelState.IsValid)
        {
            // Вернёт статус 400 Bad Request и JSON со списком ошибок
            return BadRequest(ModelState);
        }

        // Меняем событие
        eventItem.Title = eventDto.Title;
        eventItem.Description = eventDto.Description;
        eventItem.StartAt = eventDto.StartAt;
        eventItem.EndAt = eventDto.EndAt;

        _eventsService.UpdateEvent(id, eventItem);

        // Возвращаем 200
        return Ok(eventItem);
    }

    /// <summary>
    /// Удалить событие по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var eventItem = _eventsService.GetEventById(id);

        if (eventItem == null)
        {
            // Возвращаем 404
            return NotFound(); 
        }

        // Удаляем событие
        _eventsService.DeleteEvent(id);

        // Возвращаем HTTP 200 OK
        return new OkResult();
    }
}