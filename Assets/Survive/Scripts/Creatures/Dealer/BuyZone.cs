using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class BuyZone : MonoBehaviour, IInteractable
{  
    public event Action OnInteract;
    [SerializeField] IntreractableData data;
    public IntreractableData Data => data;
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
                    OnInteract?.Invoke();
                    playerMoneyHandler.Spend(ItemPrice);
                    DropLoot();
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
