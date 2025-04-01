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
    [SerializeField] public int quantity;

    /// <summary>
    /// Свойство, возвращающее количество патронов
    /// </summary>
    public int Quantity => quantity;

    private void Start()
    {
        ammoHandler = WeaponManager.Instance.GetAmmoHandlerScript();
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
            ammoHandler.CollectAmmo(quantity);
            SoundController.Instance.PlaySound(SoundType.Bullet, SoundController.Instance.inventoryAudioSource);
        }
        else
        {
            SoundController.Instance.PlaySound(SoundType.NotReady, SoundController.Instance.errorAudioSource);
        }
    }

    public void SetQuantity(int quantity)
    {
        this.quantity = quantity;
    }
}
