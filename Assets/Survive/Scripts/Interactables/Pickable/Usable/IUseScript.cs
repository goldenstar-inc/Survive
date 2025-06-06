/// <summary>
/// Интерфейс, определяющий метод класса, реализующего механику использования объекта
/// </summary>
public interface IUseScript
{
    /// <summary>
    /// Метод использования объекта
    /// </summary>
    /// <returns>True - если предмет был использован, иначе false</returns>
    public bool Use();
}
