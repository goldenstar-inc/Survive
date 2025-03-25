/// <summary>
/// Интерфейс для используемых предметов
/// </summary>
public interface IUsable : IPickable, IAmountable
{
    /// <summary>
    /// Метод использования объекта
    /// </summary>
    public IUseScript script { get; }
}
