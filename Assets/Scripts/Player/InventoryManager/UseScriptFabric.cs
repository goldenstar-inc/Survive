using UnityEditor;
using UnityEngine;

public static class UseScriptFabric
{
    public static IUseScript GetUseScript(ItemData data)
    {
        if (data is RangedWeaponItemData rangedWeaponItemData)
        {
            EquippedGun gun = new EquippedGun();
            gun.Initialize(rangedWeaponItemData);
            return gun;
        }
        else if (data is MeleeWeaponItemData meleeWeaponItemData)
        {
            EquippedMeleeWeapon knifeAttack = new EquippedMeleeWeapon();
            knifeAttack.Initialize(meleeWeaponItemData);
            return knifeAttack;
        }
        else
        {
            Debug.LogError("Use script not found!");
            return null;
        }
    }
}
