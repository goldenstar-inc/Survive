using UnityEngine;
using static HelpPhrasesModule;

/// <summary>
/// Класс, представляющий объект: "Патроны"
/// </summary>
public class Ammo : MonoBehaviour, IInteractable, IAmountable
{
    private AmmoHandler ammoHandler;
    
    /// <summary>
    /// Количество патронов
    /// </summary>
    private int quanity;

    /// <summary>
    /// Свойство, возвращающее количество патронов
    /// </summary>
    public int Quantity => quanity;

    private void Start()
    {
        ammoHandler = FindAnyObjectByType<AmmoHandler>();
        if (ammoHandler == null)
        {
            Debug.LogError("AmmoHandler not found in the scene!");
        }
    }

    /// <summary>
    /// Метод, обеспечивающий взаимодействие с объектом
    /// </summary>
    public void Interact()
    {
        if (ammoHandler.currentAmmo != ammoHandler.maxAmmo)
        {
            Destroy(gameObject);
            ammoHandler.CollectAmmo(quanity);
            SoundController.Instance.PlaySound(SoundType.Bullet, SoundController.Instance.inventoryAudioSource);
        }
        else
        {
            SoundController.Instance.PlaySound(SoundType.NotReady, SoundController.Instance.inventoryAudioSource);
        }
    }

    public void SetQuantity(int quantity)
    {
        throw new System.NotImplementedException();
    }
}
