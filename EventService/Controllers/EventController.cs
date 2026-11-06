using FluentResults;
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
    /// Метод возвращает событие по id
    /// </summary>
    /// <param name="id">Параметр индекса, для события</param>
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
        if (!ModelState.IsValid)
        {
            // Вернёт статус 400 Bad Request и JSON со списком ошибок
            return BadRequest(ModelState);
        }

        // Добавляем событие
        var newEvent = _eventsService.CreateEvent(eventDto);

        if (newEvent == null)
        {
            //Вернёт статус 400 Bad Request
            return BadRequest();
        }
        
        //Возвращаем 201
        return CreatedAtAction(nameof(GetEvent), new { id = newEvent.Id }, newEvent);   
    }

    /// <summary>
    /// Изменить событие по ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="eventDto"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id,[FromBody] EventDto eventDto)
    {
        //Валидность входных данных (Required)
        if (!ModelState.IsValid)
        {
            // Вернёт статус 400 Bad Request и JSON со списком ошибок
            return BadRequest(ModelState);
        }

        //Обновляем событие
        Result<Event> result = _eventsService.UpdateEvent(id, eventDto);

        //Если всё ок - 200
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        //Если нет, то возвращаем ошибку
        var error = result.Errors.FirstOrDefault();

        return error switch
        {
            NotFoundError => NotFound(new { message = error.Message }),
            BadRequestError => BadRequest(new { message = error.Message }),
            _ => StatusCode(500, new { message = "Произошла внутренняя ошибка сервера." })
        };
    }

    /// <summary>
    /// Удалить событие по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _eventsService.DeleteEvent(id);

        if (!result)
        {
            // Статус 404
            return NotFound();
        }

        // Статус 204
        return NoContent();
    }
}