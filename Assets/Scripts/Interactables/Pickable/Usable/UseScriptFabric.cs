using UnityEditor;
using UnityEngine;

public static class UseScriptFabric
{
    ///ПЕРЕДАВАТЬ СЮДА PLAYERCONTEXT
    public static IUseScript GetUseScript(ItemData data, HealthManager healthManager)
    {
        if (data is RangedWeaponItemData rangedWeaponItemData)
        {
            EquippedGun rangedWeapon = new EquippedGun();
            rangedWeapon.Initialize(rangedWeaponItemData);
            return rangedWeapon;
        }
        else if (data is MeleeWeaponItemData meleeWeaponItemData)
        {
            EquippedMeleeWeapon meleeWeapon = new EquippedMeleeWeapon();
            meleeWeapon.Initialize(meleeWeaponItemData);
            return meleeWeapon;
        }
        else if (data is HealableItemData healableItemData)
        {
            EquippedHealableItem healableItem = new EquippedHealableItem();
            healableItem.Initialize(healableItemData, healthManager);
            return healableItem;
        }
        else
        {
            Debug.LogError("Use script not found!");
            return null;
        }
    }
}
