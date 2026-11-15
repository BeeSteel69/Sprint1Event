namespace Sprint1;

/// <summary>
/// Класс Unit тестов для 
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
}
