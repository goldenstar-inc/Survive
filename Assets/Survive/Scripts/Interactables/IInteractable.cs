using System;

/// <summary>
/// Интерфейс, содержащий метод взаимодействия
/// </summary>
public interface IInteractable
{
    public event Action OnInteract;
    public IntreractableData Data { get; }
    bool Interact(PlayerDataProvider interactor);
}