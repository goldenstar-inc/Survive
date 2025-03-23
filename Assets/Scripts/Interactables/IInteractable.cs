/// <summary>
/// Интерфейс, содержащий метод взаимодействия
/// </summary>
public interface IInteractable
{
    public string helpPhrase { get; }
    void Interact();
}