using System;

namespace SpaceEventService;

public interface IEventService
{
        //Получить все события
        List<Event> GetAllEvents();
        //Получить событие по ID
        Event? GetEventById(int id);
        //Создать событие
        void CreateEvent(Event newEvent);
        //Обновить событие
        void UpdateEvent(int id, Event updateEvent);
        //Удалить событие
        void DeleteEvent(int id);
}
