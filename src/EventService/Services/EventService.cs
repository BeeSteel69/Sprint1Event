using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sprint1;
/// <summary>
/// Класс сервис Event
/// </summary>
public class EventService : IEventService
{
    /// <summary>
    /// Коллекция для работы с событиями
    /// </summary>
    private readonly List<Event> _events = [];

    /// <summary>
    /// Конструктор сервиса Event (по умолчанию)
    /// </summary>
    public EventService() { }

    /// <summary>
    /// Метод получения всех событий
    /// </summary>
    /// <returns></returns>
    public PaginatedResult<Event> GetAllEvents(string? title, DateTime? from, DateTime? to, int page = 1, int pageSize = 10)
    {
        if (page < 1 || pageSize < 1)
        {
            throw new ValidationException("Номер страницы (page) должен быть >= 1");
        }

        if (page < 1 || pageSize < 1)
        {
            throw new ValidationException("Размер страницы (pageSize) должен быть >= 1");
        }

        var query = _events.AsEnumerable();

        if (!string.IsNullOrEmpty(title))
        {
            query = query.Where(e => e.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }
        if (from.HasValue)
        {
            query = query.Where(e => e.StartAt.Date >= from.Value.Date);
        }
        if (to.HasValue)
        {
            query = query.Where(e => e.EndAt.Date <= to.Value.Date);
        }

        var skip = (page - 1) * pageSize;
        var total = query.Count();
        query = query.Skip(skip).Take(pageSize);

        return new PaginatedResult<Event> { Items = query.ToList(), TotalCount = total, Page = page, PageSize = pageSize };
    }

    /// <summary>
    /// Метод получения события по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Event GetEventById(int id)
    {
        var eventItem = _events.Find(e => e.Id == id);
        return eventItem != null ? eventItem : throw new InvalidOperationException("Невозможно получить несуществующее событие");
    }

    /// <summary>
    /// Метод добавления события
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public Event CreateEvent(EventDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
        {
            throw new ValidationException("Название события обязательно для заполнения");
        }

        if (dto.StartAt is null or { Year: 1 })
        {
            throw new ValidationException("Необходимо указать время начала события");
        }

        if (dto.EndAt is null or { Year: 1 })
        {
            throw new ValidationException("Необходимо указать время окончания события");
        }

        if (dto.EndAt <= dto.StartAt)
        {
            throw new ValidationException("Дата окончания (EndAt) должна быть строго позже даты начала (StartAt)");
        }

        var newEvent = new Event(dto.Title, dto.StartAt.Value, dto.EndAt.Value, description: dto.Description);

        _events.Add(newEvent);

        return newEvent;
    }

    /// <summary>
    /// Метод изменения события по ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public Event UpdateEvent(int id, EventDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
        {
            throw new ValidationException("Название события обязательно для заполнения");
        }

        if (dto.StartAt is null or { Year: 1 })
        {
            throw new ValidationException("Необходимо указать время начала события");
        }

        if (dto.EndAt is null or { Year: 1 })
        {
            throw new ValidationException("Необходимо указать время окончания события");
        }

        if (dto.EndAt <= dto.StartAt)
        {
            throw new ValidationException("Дата окончания (EndAt) должна быть строго позже даты начала (StartAt)");
        }

        var foundIndex = _events.FindIndex(e => e.Id == id);
        if (foundIndex == -1)
        {
            throw new InvalidOperationException("Невозможно обновить несуществующее событие");
        }

        var newEvent = _events[foundIndex];
        newEvent.Title = dto.Title;
        newEvent.Description = dto.Description;
        newEvent.StartAt = dto.StartAt.Value;
        newEvent.EndAt = dto.EndAt.Value;

        return newEvent;
    }

    /// <summary>
    /// Метод удаления события по ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public bool DeleteEvent(int id)
    {
        var eventItem = _events.Find(e => e.Id == id);
        if (eventItem == null)
        {
            throw new InvalidOperationException("Невозможно удалить несуществующее событие");
        }

        _events.Remove(eventItem);
        return true;
    }
}