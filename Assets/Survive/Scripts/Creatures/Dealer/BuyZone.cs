using UnityEngine;
using static HelpPhrasesModule;
public class BuyZone : MonoBehaviour, IInteractable
{  
    /// <summary>
    /// Точка спавна предметов
    /// </summary>
    [SerializeField] Transform spawnPoint;

    /// <summary>
    /// Точка для воспроизведения звука
    /// </summary>
    [SerializeField] AudioSource audioSource;

    /// <summary>
    /// Массив объектов которые могут выпасть
    /// </summary>
    public GameObject[] itemsToDrop;

    /// <summary>
    /// Вспомогательная фраза
    /// </summary>
    public string helpPhrase => actionToPhrase[Action.Buy];

    /// <summary>
    /// Цена за предмет
    /// </summary>
    private int ItemPrice = 50;
    [SerializeField] AudioClip dealerSpeech;

    /// <summary>
    /// Старт
    /// </summary>
    private void Start()
    {
        if (spawnPoint == null)
        {
            Debug.LogWarning("PlayerTransform not set");
        }
    }

    /// <summary>
    /// Взаимодействие с объектом
    /// </summary>
    public bool Interact(PlayerDataProvider interactor)
    {
        if (interactor != null && interactor is IMoneyProvider moneyProvider)
        {
            MoneyHandler playerMoneyHandler = moneyProvider.MoneyHandler;

            if (playerMoneyHandler != null)
            {
                if (playerMoneyHandler.GetCurrentBalance() >= ItemPrice)
                {
                    playerMoneyHandler.Spend(ItemPrice);
                    DropLoot();
                    //interactor.SoundController.PlayAudioClip(dealerSpeech);
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Метод, отвечающий за выдачу лута
    /// </summary>
    private void DropLoot()
    {
        if (itemsToDrop != null)
        {
            int randomInRange = Random.Range(0, itemsToDrop.Length);
            
            if (itemsToDrop[randomInRange] != null)
            {
                Instantiate(itemsToDrop[randomInRange], spawnPoint.position, Quaternion.identity);
            }
        }
    }
}
