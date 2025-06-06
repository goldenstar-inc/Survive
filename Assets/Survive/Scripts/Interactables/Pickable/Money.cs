using UnityEngine;

/// <summary>
/// Класс, представляющий объект: "Деньги"
/// </summary>
public class Money : PickableItem
{
    public override bool Interact(PlayerDataProvider interactor)
    {
        if (interactor != null && interactor is IMoneyProvider moneyProvider)
        {
            MoneyHandler moneyHandler = moneyProvider.MoneyHandler;
            
            if (moneyHandler != null)
            {
                InvokeInteract();
                moneyHandler.Collect(Random.Range(20, 50));
                Destroy(transform.root.gameObject);
                return true;
            }
        }
        return false;
    }
} 