using System.ComponentModel.DataAnnotations;

namespace SpaceEventService;
public class EventDto
{

    [Required(ErrorMessage = "Название события обязательно для заполнения")]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [Required(ErrorMessage = "Необходимо указать время начала события")]
    public DateTime StartAt { get; set; }

    [Required(ErrorMessage = "Необходимо указать время окончания события")]
    public DateTime EndAt { get; set; }
}
