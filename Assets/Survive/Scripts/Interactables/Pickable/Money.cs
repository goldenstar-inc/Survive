using UnityEngine;

/// <summary>
/// Класс, представляющий объект: "Деньги"
/// </summary>
public class Money : PickableItem
{
    public override bool Interact(PlayerDataProvider interactor)
    {
        if (interactor != null)
        {
            MoneyHandler moneyHandler = interactor.MoneyHandler;

            if (moneyHandler != null)
            {
                moneyHandler.AddMoney(Random.Range(20, 50));
                Destroy(gameObject);
                interactor.SoundController?.PlayAudioClip(PickSound);
                return true;
            }
        }
        return false;
    }
} 