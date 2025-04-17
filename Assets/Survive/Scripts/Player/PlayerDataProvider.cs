using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

/// <summary>
/// Интерфейс, содержащий ссылки на компоненты игрока
/// </summary>
public class PlayerDataProvider : MonoBehaviour, IPlayerSettingProvider, IHealthProvider, IAmmoProvider, IMoneyProvider, ISoundProvider, IWeaponProvider, IQuestProvider, IDialogueProvider
{
    public PlayerSetting PlayerSetting { get; private set; }
    public HealthManager HealthManager { get; private set; }
    public AmmoHandler AmmoHandler { get; private set; }
    public MoneyHandler MoneyHandler { get; private set; }
    public SoundController SoundController { get; private set; }
    public WeaponManager WeaponManager { get; private set; }
    public QuestManager QuestManager { get; private set; }
    public DialogueManager DialogueManager { get; private set; }

    /// <summary>
    /// Инициализация фасада
    /// </summary>
    /// <param name="PlayerSetting">Конфиг игрока</param>
    /// <param name="HealthManager">Скрипт, управляющий здоровьем игрока</param>
    /// <param name="AmmoHandler">Скрипт, управляющий боеприпасами игрока</param>
    /// <param name="MoneyHandler">Скрипт, управляющий денежным балансом игрока</param>
    /// <param name="SoundController">Скрипт, управляющий звуками игрока</param>
    /// <param name="WeaponManager">Скрипт, управляющий оружием игрока</param>
    /// <param name="QuestManager">Скрипт, управляющий квестами</param>
    public void Init(
        PlayerSetting PlayerSetting,
        HealthManager HealthManager,
        AmmoHandler AmmoHandler,
        MoneyHandler MoneyHandler,
        SoundController SoundController,
        WeaponManager WeaponManager,
        QuestManager QuestManager,
        DialogueManager DialogueManager
        )
    {
        this.PlayerSetting = PlayerSetting;
        this.HealthManager = HealthManager;
        this.AmmoHandler = AmmoHandler;
        this.MoneyHandler = MoneyHandler;
        this.SoundController = SoundController;
        this.WeaponManager = WeaponManager;
        this.QuestManager = QuestManager;
        this.DialogueManager = DialogueManager;
    }
}
