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
                moneyHandler.Collect(Random.Range(20, 50));
                Destroy(gameObject);

                if (interactor is ISoundProvider soundProvider)
                {
                    soundProvider.SoundController?.PlayAudioClip(PickSound);
                }
                return true;
            }
        }
        return false;
    }
} 