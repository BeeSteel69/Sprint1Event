using System;

namespace SpaceEventService;

public class EventService : IEventService
{
    // Коллекция для работы с событиями
    public static List<Event> _events= [];

    //Конструктор
    public EventService() { }
    public EventService(List <Event> events)
    {
        _events = events;
    }

    // Метод получения всех событий
    public List<Event> GetAllEvents()
    {
        return _events;
    }

    // Метод получения события по id
    public Event? GetEventById(int id)
    {
        return _events.Find(e => e.Id == id);
    }

    // Метод добавления события
    public void CreateEvent(Event newEvent)
    {
        _events.Add(newEvent);
    }

    // Метод изменения события
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
    // Метод удаления события по id
    public void DeleteEvent(int id)
    {
        var eventItem= _events[id];
        _events.Remove(eventItem);
    }
}