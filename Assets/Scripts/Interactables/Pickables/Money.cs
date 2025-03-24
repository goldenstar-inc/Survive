using UnityEngine;
using static HelpPhrasesModule;
using static InventoryController;
/// <summary>
/// Класс, представляющий объект: "Деньги"
/// </summary>
public class Money : MonoBehaviour, IInteractable
{
    /// <summary>
    /// Свойство, хранящее подсказку для игрока
    /// </summary>
    public string helpPhrase => actionToPhrase[Action.PickUp];

    /// <summary>
    /// Метод, обеспечивающий взаимодействие с объектом
    /// </summary>
    public void Interact()
    {
        Destroy(gameObject);

        MoneyHandler.Instance.AddMoney(Random.Range(20, 50));
        MoneyHandler.Instance.UpdateUI();
    }
}