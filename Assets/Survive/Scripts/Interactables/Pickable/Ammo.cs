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
    public override bool Interact(IPlayerDataProvider interactor)
    {
        if (interactor != null)
        {
            AmmoHandler handler = interactor.AmmoHandler;

            if (handler != null)
            {
                if (handler.currentAmmo != handler.maxAmmo)
                {
                    handler.CollectAmmo(Quantity);
                    Destroy(gameObject);
                    interactor.SoundController?.PlaySound(PickSound);
                    return true;
                }
            }
        }
        return false;
    }
}
