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
    public ActionResult<List<Event>> GetAllEvents(
        [FromQuery] string? title, 
        [FromQuery] DateTime? from, 
        [FromQuery] DateTime? to,
        [FromQuery] int page = 1, 
        [FromQuery] int pageSize = 10)
    {
        return Ok(_eventsService.GetAllEvents(title,from,to,page,pageSize));
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

        if (eventItem.IsSuccess)
        {
            // Возвращаем 200
            return Ok(eventItem);            
        }

        var error = eventItem.Errors.FirstOrDefault();

        return error switch
        {
            NotFoundError => NotFound(new { message = error.Message }),
            _ => StatusCode(500, new { message = "Произошла внутренняя ошибка сервера." })
        };

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
        Result<Event> result = _eventsService.CreateEvent(eventDto);

        //Возвращаем 201
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
                
        var error = result.Errors.FirstOrDefault();

        return error switch
        {
            BadRequestError => BadRequest(new { message = error.Message }),
            _ => StatusCode(500, new { message = "Произошла внутренняя ошибка сервера." })
        };
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

        if (result.IsSuccess)
        {
            // Статус 204
            return NoContent();            
        }
        //Если нет, то возвращаем ошибку
        var error = result.Errors.FirstOrDefault();

        return error switch
        {
            NotFoundError => NotFound(new { message = error.Message }),
            _ => StatusCode(500, new { message = "Произошла внутренняя ошибка сервера." })
        };

    }
}