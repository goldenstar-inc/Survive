using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

/// <summary>
/// Интерфейс, содержащий ссылки на компоненты игрока
/// </summary>
public class PlayerDataProvider : MonoBehaviour, IPlayerSettingProvider, IHealthProvider, IAmmoProvider, IMoneyProvider, IWeaponProvider, IQuestProvider, IDialogueProvider, IInventoryProvider
{
    public PlayerSetting PlayerSetting { get; private set; }
    public HealthHandler HealthHandler { get; private set; }
    public AmmoHandler AmmoHandler { get; private set; }
    public MoneyHandler MoneyHandler { get; private set; }
    public WeaponManager WeaponManager { get; private set; }
    public QuestManager QuestManager { get; private set; }
    public DialogueManager DialogueManager { get; private set; }
    public Inventory Inventory { get; private set; }

    /// <summary>
    /// Инициализация фасада
    /// </summary>
    /// <param name="PlayerSetting">Конфиг игрока</param>
    /// <param name="HealthManager">Скрипт, управляющий здоровьем игрока</param>
    /// <param name="AmmoHandler">Скрипт, управляющий боеприпасами игрока</param>
    /// <param name="MoneyHandler">Скрипт, управляющий денежным балансом игрока</param>
    /// <param name="WeaponManager">Скрипт, управляющий оружием игрока</param>
    /// <param name="QuestManager">Скрипт, управляющий квестами</param>
    /// <param name="Inventory">Класс инвентаря</param>
    public void Init(
        PlayerSetting PlayerSetting,
        HealthHandler HealthManager,
        AmmoHandler AmmoHandler,
        MoneyHandler MoneyHandler,
        WeaponManager WeaponManager,
        QuestManager QuestManager,
        DialogueManager DialogueManager,
        Inventory Inventory
        )
    {
        this.PlayerSetting = PlayerSetting;
        this.HealthHandler = HealthManager;
        this.AmmoHandler = AmmoHandler;
        this.MoneyHandler = MoneyHandler;
        this.WeaponManager = WeaponManager;
        this.QuestManager = QuestManager;
        this.DialogueManager = DialogueManager;
        this.Inventory = Inventory;
    }
}
