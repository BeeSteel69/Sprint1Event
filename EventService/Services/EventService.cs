using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;
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
    private readonly List<Event> _events= [];

    /// <summary>
    /// Конструктор сервиса Event (по умолчанию)
    /// </summary>
    public EventService() { }

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
    /// <param name="dto"></param>
    public Event CreateEvent(EventDto dto)
    {
        if(dto.EndAt <= dto.StartAt)
        {
            return null;
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
    /// <exception cref="ArgumentException"></exception>
    public Result<Event> UpdateEvent(int id, EventDto dto)
    {
        var foundIndex = _events.FindIndex(e => e.Id == id);
        if (foundIndex == -1)
        {
            return Result.Fail<Event>(new NotFoundError($"Невозможно обновить несуществующее событие {id}"));
        }
       
        if (dto.EndAt <= dto.StartAt)
        {
            // Добавление простой текстовой ошибки
            return Result.Fail<Event>(new BadRequestError("Дата окончания (EndAt) должна быть строго позже даты начала (StartAt)"));
        }

        var newEvent = _events[foundIndex];
        newEvent.Title = dto.Title;
        newEvent.Description = dto.Description;
        newEvent.StartAt = dto.StartAt.Value;
        newEvent.EndAt = dto.EndAt.Value;

        _events[foundIndex] = newEvent;

        return Result.Ok(newEvent);             
    }

    /// <summary>
    /// Метод удаления события по ID
    /// </summary>
    /// <param name="id"></param>
    public bool DeleteEvent(int id)
    {
        var eventItem= _events.Find(e => e.Id == id);
        if (eventItem == null)
        {
            return false;
        }

        _events.Remove(eventItem);
        return true;
    }
}

/// <summary>
/// Кастомный класс ошибки NotFoundError
/// </summary>
public class NotFoundError : Error
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="message"></param>
    public NotFoundError(string message) : base(message) { }
}

/// <summary>
/// Кастомный класс ошибки BadRequestError
/// </summary>
public class BadRequestError : Error
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="message"></param>
    public BadRequestError(string message) : base(message) { }
}