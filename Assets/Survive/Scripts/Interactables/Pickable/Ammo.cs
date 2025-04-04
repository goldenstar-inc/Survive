using System;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

/// <summary>
/// Класс, представляющий объект: "Патроны"
/// </summary>
public class Ammo : PickableItem
{
    /// <summary>
    /// Переопределенный метод, обеспечивающий взаимодействие с объектом
    /// </summary>
    public override void Interact(IPlayerDataProvider interactor)
    {
        if (interactor == null)
        {
            Debug.LogError("The interacting creature is null!");
            return;
        }

        AmmoHandler handler = interactor.ammoHandler;
        
        if (handler == null)
        {
            Debug.LogError("The AmmoHandler of the interacting player isn't properly set!");
            return;
        }

        if (handler.currentAmmo != handler.maxAmmo)
        {
            Destroy(gameObject);
            handler.CollectAmmo(1);
            SoundController.Instance.PlaySound(SoundType.Bullet, SoundController.Instance.inventoryAudioSource);
        }
        else
        {
            SoundController.Instance.PlaySound(SoundType.NotReady, SoundController.Instance.errorAudioSource);
        }
    }
}
