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
    public override bool Interact(PlayerDataProvider interactor)
    {
        if (interactor != null && interactor is IAmmoProvider ammoProvider)
        {
            AmmoHandler ammoHandler = ammoProvider.AmmoHandler;

            if (ammoHandler != null)
            {
                if (ammoHandler.currentAmmo != ammoHandler.maxAmmo)
                {
                    ammoHandler.CollectAmmo(1);
                    Destroy(gameObject);
                    return true;
                }
            }
        }
        return false;
    }
}
