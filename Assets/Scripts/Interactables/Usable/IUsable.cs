/// <summary>
/// Интерфейс для используемых предметов
/// </summary>
public interface IUsable : IPickable
{
    /// <summary>
    /// Свойство, хранящее количество данного предмета
    /// </summary>
    public int Quanity { get; set; }

    /// <summary>
    /// Метод использования объекта
    /// </summary>
    public IUseScript script { get; }
}
