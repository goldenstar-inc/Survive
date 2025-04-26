using System;
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
                    InvokeInteract();
                    ammoHandler.CollectAmmo(1);
                    Destroy(transform.root.gameObject);
                    return true;
                }
            }
        }
        return false;
    }
}
