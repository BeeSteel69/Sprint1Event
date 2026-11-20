namespace Sprint1;

/// <summary>
/// DTO для возврата результата пагинации
/// </summary>
/// <typeparam name="T"></typeparam>
public class PaginatedResult<T>
{
    /// <summary>
    /// Список событий
    /// </summary>
    public List<T> Items { get; set; } = [];
    /// <summary>
    /// Общее кол-во событий
    /// </summary>
    public int TotalCount { get; set; }
    /// <summary>
    /// Номер текущей страницы
    /// </summary>
    public int Page { get; set; }
    /// <summary>
    /// Размер страницы
    /// </summary>
    public int PageSize { get; set; }
    /// <summary>
    /// Количество элементов на текущей странице
    /// </summary>
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}
