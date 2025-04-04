using UnityEngine;

/// <summary>
/// Класс, представляющий объект: "Деньги"
/// </summary>
public class Money : PickableItem
{
    /// <summary>
    /// Переопределенный метод, обеспечивающий взаимодействие с объектом
    /// </summary>
    public override void Interact(IPlayerDataProvider interactor)
    {
        Destroy(gameObject);
        MoneyHandler.Instance.AddMoney(Random.Range(20, 50));
        SoundController.Instance.PlaySound(SoundType.Money, SoundController.Instance.inventoryAudioSource);
    }
}