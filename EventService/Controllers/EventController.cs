using Microsoft.AspNetCore.Mvc;

namespace SpaceEventService;

[ApiController]
[Route("api/[controller]")]
// Получаем доступ к EventService с помощью первичного конструктора
public class EventsController(IEventService _eventsService) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Event>> GetAllEvents()
    {
        // Получаем все события
        return _eventsService.GetAllEvents();
    }

    [HttpGet("{id:int}")]
    public ActionResult<Event> GetEvent(int id)
    {
        var eventItem=_eventsService.GetEventById(id);

        if (eventItem == null)
        {
            return NotFound();// Возвращаем 404
        }

        return Ok(eventItem);// Возвращаем 200
    }

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
        
        return new CreatedResult();// Возвращаем 201
    }

    [HttpPut("events/{id}")]
    public ActionResult Update(int id,[FromBody] EventDto eventDto)
    {
            
        var eventItem = _eventsService.GetEventById(id);

        if (eventItem == null)
        {
            return NotFound();// Возвращаем 404
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

        return Ok(eventItem);// Возвращаем 200
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var eventItem = _eventsService.GetEventById(id);

        if (eventItem == null)
        {
            return NotFound(); // Возвращаем 404
        }

        // Удаляем событие
        _eventsService.DeleteEvent(id);

        // Возвращаем HTTP 200 OK
        return new OkResult();
    }
}