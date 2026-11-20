using System.ComponentModel.DataAnnotations;

namespace Sprint1;

/// <summary>
/// DTO для создания нового события
/// </summary>
public class EventDto
{
    /// <summary>
    /// Название события
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Описание события (не обязательно)
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Дата начала события
    /// </summary>
    public DateTime? StartAt { get; set; }

    /// <summary>
    /// Дата окончания события
    /// </summary>
    public DateTime? EndAt { get; set; }
}
