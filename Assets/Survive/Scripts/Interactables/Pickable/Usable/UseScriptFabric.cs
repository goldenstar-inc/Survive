using UnityEditor;
using UnityEngine;

public static class UseScriptFabric
{
    ///ПЕРЕДАВАТЬ СЮДА PLAYERCONTEXT
    public static IUseScript GetUseScript(PickableItemData itemData, PlayerDataProvider playerData)
    {
        if (itemData is RangedWeaponItemData rangedWeaponItemData)
        {
            EquippedGun rangedWeapon = new EquippedGun();
            rangedWeapon.Initialize(rangedWeaponItemData, playerData);
            return rangedWeapon;
        }
        else if (itemData is MeleeWeaponItemData meleeWeaponItemData)
        {
            EquippedMeleeWeapon meleeWeapon = new EquippedMeleeWeapon();
            meleeWeapon.Initialize(meleeWeaponItemData, playerData);
            return meleeWeapon;
        }
        else if (itemData is HealableItemData healableItemData)
        {
            EquippedHealableItem healableItem = new EquippedHealableItem();
            healableItem.Initialize(healableItemData, playerData);
            return healableItem;
        }
        else
        {
            Debug.LogError("Use script not found!");
            return null;
        }
    }
}
