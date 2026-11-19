using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Sprint1;

/// <summary>
/// Класс Unit тестов для EventService
/// </summary>
public class UnitTest1
{
    /// <summary>
    /// Тест успешного создания события
    /// </summary>
    [Fact]
    public void CreateEvent_WhenValidInput_ReturnsEvent()
    {
        // Arrange
        var service = new EventService();
        var dto = new EventDto { Title = "Test", StartAt = DateTime.Now, EndAt = DateTime.Now.AddHours(3) };
        // Act
        var result = service.CreateEvent(dto);
        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test", result.Title);
    }

    /// <summary>
    /// Тест создания события с некорректными данными
    /// </summary>
    [Theory]
    [InlineData("EmptyTitle", "Название события обязательно для заполнения")]
    [InlineData("NullStart", "Необходимо указать время начала события")]
    [InlineData("DefaultStart", "Необходимо указать время начала события")]
    [InlineData("NullEnd", "Необходимо указать время окончания события")]
    [InlineData("EndBeforeStart", "Дата окончания (EndAt) должна быть строго позже даты начала (StartAt)")]
    public void CreateEvent_InvalidScenarios_ThrowsValidationExceptionWithExpectedMessage(string scenario, string expectedMessage)
    {
        // Arrange
        var service = new EventService();
        var validStart = new DateTime(2026, 11, 20, 10, 0, 0);
        var validEnd = new DateTime(2026, 11, 20, 12, 0, 0);

        var dto = scenario switch
        {
            "EmptyTitle" => new EventDto { Title = "", StartAt = validStart, EndAt = validEnd },
            "NullStart" => new EventDto { Title = "Тест", StartAt = null, EndAt = validEnd },
            "DefaultStart" => new EventDto { Title = "Тест", StartAt = default(DateTime), EndAt = validEnd },
            "NullEnd" => new EventDto { Title = "Тест", StartAt = validStart, EndAt = null },
            "EndBeforeStart" => new EventDto { Title = "Тест", StartAt = validEnd, EndAt = validStart },
            _ => throw new ArgumentOutOfRangeException(nameof(scenario))
        };

        // Act
        Action act = () => service.CreateEvent(dto);

        // Assert
        var ex = Assert.Throws<ValidationException>(act);
        Assert.Equal(expectedMessage, ex.Message);
    }

    /// <summary>
    /// Тест получения всех событий
    /// </summary>
    [Fact]
    public void GetAllEvents_ReturnsAllStoredEvents()
    {
        // Arrange
        var service = new EventService(); 
        var dto = new EventDto { Title = "Test", StartAt = DateTime.Now, EndAt = DateTime.Now.AddHours(10), Description = "TestDesc" };
        // Act
        service.CreateEvent(dto); 
        var events = service.GetAllEvents(null,null,null);
        // Assert
        Assert.Single(events.Items); 
        Assert.Equal("Test", events.Items[0].Title);
    }

    /// <summary>
    /// Тест получения события по ID
    /// </summary>
    [Fact]
    public void GetEventById_ExistingId_ReturnsEvent()
    {
        // Arrange
        var service = new EventService(); 
        var dto = new EventDto { Title = "Test1", StartAt = DateTime.Now, EndAt = DateTime.Now.AddHours(10), Description = "TestDesc" }; 
        var created = service.CreateEvent(dto);
        // Act
        var result = service.GetEventById(created.Id);
        // Assert
        Assert.NotNull(result); 
        Assert.Equal(created.Id, result.Id);
        Assert.Equal("Test1", result.Title);
    }

    /// <summary>
    /// Тест получения несуществующего события по ID
    /// </summary>
    [Fact]
    public void GetEventById_NonExistingId_ThrowsInvalidOperationExceptionWithExpectedMessage()
    {
        // Arrange
        var service = new EventService();
        // Act
        Action act = () => service.GetEventById(-1);
        // Assert
        var ex = Assert.Throws<InvalidOperationException>(act);
        Assert.Equal("Невозможно получить несуществующее событие", ex.Message);
    }

    /// <summary>
    /// Тест обновления существующего события
    /// </summary>
    [Fact]
    public void UpdateEvent_ExistingId_UpdatesData()
    {
        // Arrange
        var service = new EventService();
        var dto = new EventDto { Title = "Old", StartAt = DateTime.Now, EndAt = DateTime.Now.AddHours(1) };
        var created = service.CreateEvent(dto);
        var updateDto = new EventDto { Title = "New", StartAt = created.StartAt, EndAt = created.EndAt };
        // Act
        var result = service.UpdateEvent(created.Id, updateDto);
        // Assert
        Assert.NotNull(result);
        Assert.Equal("New", service.GetEventById(created.Id).Title);
    }

    /// <summary>
    /// Тест обновления несуществующего события
    /// </summary>
    [Fact]
    public void UpdateEvent_NonExistingId_ThrowsInvalidOperationExceptionWithExpectedMessage()
    { 
        // Arrange
        var service = new EventService(); 
        var dto = new EventDto { Title = "New Event", StartAt = DateTime.Now, EndAt = DateTime.Now.AddHours(1) }; 
        var initialCount = service.GetAllEvents(null,null,null).Items.Count;
        // Act
        Action act = () => service.UpdateEvent(-1, dto);
        // Assert
        var ex = Assert.Throws<InvalidOperationException>(act);
        Assert.Equal("Невозможно обновить несуществующее событие", ex.Message);
        Assert.Equal(initialCount, service.GetAllEvents(null, null, null).Items.Count);
    }

    /// <summary>
    /// Тест обновления события с некорректными данными
    /// </summary>
    [Theory]
    [InlineData("EmptyTitle", "Название события обязательно для заполнения")]
    [InlineData("NullStart", "Необходимо указать время начала события")]
    [InlineData("DefaultStart", "Необходимо указать время начала события")]
    [InlineData("NullEnd", "Необходимо указать время окончания события")]
    [InlineData("EndBeforeStart", "Дата окончания (EndAt) должна быть строго позже даты начала (StartAt)")]
    public void UpdateEvent_InvalidDates_ThrowsValidationOperationExceptionWithExpectedMessage(string scenario, string expectedMessage)
    { 
        // Arrange
        var service = new EventService(); 
        var originalDto = new EventDto { Title = "Old", StartAt = DateTime.Now, EndAt = DateTime.Now.AddHours(1) }; 
        var created = service.CreateEvent(originalDto);
        var validStart = new DateTime(2026, 11, 20, 10, 0, 0);
        var validEnd = new DateTime(2026, 11, 20, 12, 0, 0);
        var invalidDto = scenario switch
        {
            "EmptyTitle" => new EventDto { Title = "", StartAt = validStart, EndAt = validEnd },
            "NullStart" => new EventDto { Title = "Тест", StartAt = null, EndAt = validEnd },
            "DefaultStart" => new EventDto { Title = "Тест", StartAt = default(DateTime), EndAt = validEnd },
            "NullEnd" => new EventDto { Title = "Тест", StartAt = validStart, EndAt = null },
            "EndBeforeStart" => new EventDto { Title = "Тест", StartAt = validEnd, EndAt = validStart },
            _ => throw new ArgumentOutOfRangeException(nameof(scenario))
        };
        // Act
        Action act = () => service.UpdateEvent(created.Id, invalidDto);
        // Assert
        var ex = Assert.Throws<ValidationException>(act);
        Assert.Equal(expectedMessage, ex.Message);
    }

    /// <summary>
    /// Тест удаления существующего события
    /// </summary>
    [Fact]
    public void DeleteEvent_ExistingId_RemovesEventAndReturnsFailure()
    {
        // Arrange
        var service = new EventService();
        var dto = new EventDto { Title = "DeleteTest", StartAt = DateTime.Now, EndAt = DateTime.Now.AddHours(2) };
        var created = service.CreateEvent(dto);
        // Act
        var result = service.DeleteEvent(created.Id);
        var allEvents = service.GetAllEvents(null,null,null);
        // Assert
        Assert.True(result);
        Assert.Empty(allEvents.Items);
    }

    /// <summary>
    /// Тест удаления несуществующего события
    /// </summary>
    [Fact]
    public void DeleteEvent_NonExistingId_ThrowsInvalidOperationExceptionWithExpectedMessage()
    {
        // Arrange
        var service = new EventService();
        var dto = new EventDto { Title = "DeleteTest", StartAt = DateTime.Now, EndAt = DateTime.Now.AddHours(2) };
        service.CreateEvent(dto);
        // Act
        Action act = () => service.DeleteEvent(-1);
        var allEvents = service.GetAllEvents(null,null,null);
        // Assert
        var ex = Assert.Throws<InvalidOperationException>(act);
        Assert.Equal("Невозможно удалить несуществующее событие", ex.Message);
        Assert.Single(allEvents.Items);
    }

    /// <summary>
    /// Тест фильтрации по названию
    /// </summary>
    [Fact]
    public void GetAllEvents_WithTitleFilter_ReturnsMatchingEvents()
    {
        //Arrange
        var service = new EventService();
        service.CreateEvent(new EventDto { Title = "Event Small", StartAt = DateTime.Now, EndAt = DateTime.Now.AddHours(2) });
        service.CreateEvent(new EventDto { Title = "Event Big", StartAt = DateTime.Now, EndAt = DateTime.Now.AddHours(2) });
        service.CreateEvent(new EventDto { Title = "Event BiggieSmalls", StartAt = DateTime.Now, EndAt = DateTime.Now.AddHours(2) });
        // Act
        var result = service.GetAllEvents("small",null,null);
        // Assert
        Assert.Equal(2, result.Items.Count);
        Assert.Contains(result.Items, e => e.Title.Equals("Event Small", StringComparison.OrdinalIgnoreCase));
        Assert.Contains(result.Items, e => e.Title.Equals("Event BiggieSmalls", StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Тест фильтрации по датам 
    /// </summary>
    [Fact]
    public void GetFilteredEvents_ByDateRange_ReturnsCorrectEvents()
    { 
        // Arrange
        var service = new EventService(); var baseTime = DateTime.Now.Date;
        service.CreateEvent(new EventDto { Title = "Before", StartAt = baseTime.AddDays(-2), EndAt = baseTime.AddDays(-1) });
        service.CreateEvent(new EventDto { Title = "Inside", StartAt = baseTime.AddDays(-1), EndAt = baseTime.AddDays(1) });
        service.CreateEvent(new EventDto { Title = "After", StartAt = baseTime.AddDays(2), EndAt = baseTime.AddDays(3) });
        var startDate = baseTime.AddDays(-1);
        var endDate = baseTime.AddDays(1);
        // Act 
        // Предполагаем, что у сервиса есть метод с параметрами startDate/endDate
        var result = service.GetAllEvents(null,startDate,endDate);
        // Assert
        Assert.Single(result.Items);
        Assert.Equal("Inside", result.Items[0].Title); 
    }

    /// <summary>
    /// Тест пагинации событий
    /// </summary>
    [Fact]
    public void GetPagedEvents_ReturnsCorrectPage()
    { 
        // Arrange
        var service = new EventService();
        for (int i = 0; i < 25; i++) 
            service.CreateEvent(new EventDto { Title = $"Event {i}", StartAt = DateTime.Now, EndAt = DateTime.Now.AddHours(1) });
        int page = 2, pageSize = 10;
        // Act
        var result = service.GetAllEvents(null,null,null, page, pageSize);
        // Assert
        Assert.Equal(10, result.Items.Count);
        Assert.Equal(25, result.TotalCount);
        Assert.Equal(page, result.Page);
        Assert.Equal(pageSize, result.PageSize);
        Assert.Equal("Event 10", result.Items[0].Title);
    }

    /// <summary>
    /// Тест комбинированной фильтрация
    /// </summary>
    [Fact]
    public void GetFilteredEvents_CombinedFilters_AppliesAllConditions()
    {
        // Arrange
        var service = new EventService(); 
        service.CreateEvent(new EventDto { Title = "Match", StartAt = DateTime.Now, EndAt = DateTime.Now.AddHours(1) }); 
        service.CreateEvent(new EventDto { Title = "NoTitle", StartAt = DateTime.Now, EndAt = DateTime.Now.AddHours(1) });
        service.CreateEvent(new EventDto { Title = "Match", StartAt = DateTime.Now.AddDays(-5), EndAt = DateTime.Now.AddDays(-4) });

        // Act
        var result = service.GetAllEvents("match", DateTime.Now.Date, DateTime.Now.AddHours(1), 1, 10);
        // Assert
        Assert.Equal(1, result.TotalCount);
        Assert.Single(result.Items); 
        Assert.Equal("Match", result.Items[0].Title); 
    }

}
