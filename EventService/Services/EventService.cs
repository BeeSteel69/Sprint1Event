using System;

namespace Sprint1;
/// <summary>
/// Класс сервис Event
/// </summary>
public class EventService : IEventService
{
    /// <summary>
    /// Коллекция для работы с событиями
    /// </summary>
    public static List<Event> _events= [];

    /// <summary>
    /// Конструктор сервиса Event (по умолчанию)
    /// </summary>
    public EventService() { }

    /// <summary>
    /// Конструктор сервиса Event
    /// </summary>
    /// <param name="events"></param>
    public EventService(List <Event> events)
    {
        _events = events;
    }

    /// <summary>
    /// Метод получения всех событий
    /// </summary>
    /// <returns></returns>
    public List<Event> GetAllEvents()
    {
        return _events;
    }

    /// <summary>
    /// Метод получения события по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Event? GetEventById(int id)
    {
        return _events.Find(e => e.Id == id);
    }

    /// <summary>
    /// Метод добавления события
    /// </summary>
    /// <param name="newEvent"></param>
    public void CreateEvent(Event newEvent)
    {
        _events.Add(newEvent);
    }

    /// <summary>
    /// Метод изменения события по ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateEvent"></param>
    /// <exception cref="ArgumentException"></exception>
    public void UpdateEvent(int id, Event updateEvent)
    {
        var foundIndex = _events.FindIndex(e => e.Id == id);
        if (foundIndex == -1)
        {
            throw new ArgumentException($"Событие с ID {id} не найдено.");
        }
        else
            _events[foundIndex] = updateEvent;
    }

    /// <summary>
    /// Метод удаления события по ID
    /// </summary>
    /// <param name="id"></param>
    public void DeleteEvent(int id)
    {
        var eventItem= _events[id];
        _events.Remove(eventItem);
    }
}