using UnityEngine;
using static HelpPhrasesModule;
public class BuyZone : MonoBehaviour, IInteractable
{
    /// <summary>
    /// Массив объектов которые могут выпасть
    /// </summary>
    public GameObject[] itemsToDrop;
    /// <summary>
    /// Вспомогательная фраза
    /// </summary>
    public string helpPhrase => actionToPhrase[Action.Buy];

    /// <summary>
    /// Transform игрока
    /// </summary>
    private Transform playerTransform;

    /// <summary>
    /// Transform игрока
    /// </summary>
    private MoneyHandler playerMoneyHandler;

    /// <summary>
    /// Цена за предмет
    /// </summary>
    private int ItemPrice = 50;

    /// <summary>
    /// Старт
    /// </summary>
    private void Start()
    {
        playerTransform = FindAnyObjectByType<InventoryController>()?.transform;
        playerMoneyHandler = FindAnyObjectByType<MoneyHandler>();
        
        if (playerTransform == null)
        {
            Debug.LogWarning("PlayerTransform not set");
        }

        if (playerMoneyHandler == null)
        {
            Debug.LogWarning("PlayerMoneyHandler not set");
        }
    }

    /// <summary>
    /// Взаимодействие с объектом
    /// </summary>
    public void Interact(IPlayerDataProvider interactor)
    {
        if (playerMoneyHandler.Balance >= ItemPrice)
        {
            playerMoneyHandler.Pay(ItemPrice);
            DropLoot();

            if (!SoundController.Instance.dealerAudioSource.isPlaying)
            {
                SoundController.Instance.PlaySound(SoundType.DealerSpeech, SoundController.Instance.dealerAudioSource);
            }
        }
        else
        {
            SoundController.Instance.PlaySound(SoundType.NotReady, SoundController.Instance.errorAudioSource);
        }
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
                Instantiate(itemsToDrop[randomInRange], playerTransform.position, Quaternion.identity);
            }
        }
    }
}
