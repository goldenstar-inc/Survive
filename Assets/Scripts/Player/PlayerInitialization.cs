using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, устанавливающий подписчков на события для игрока
/// </summary>
public class PlayerInitialization : MonoBehaviour
{
    /// <summary>
    /// Cкрипт, отвечающий за отображение здоровья игрока
    /// </summary>
    public HealthDisplay healthDisplay;

    /// <summary>
    /// Скрипт, отвечающий за отображение анимации игрока
    /// </summary>
    public PlayerAnimationController playerAnimationController;

    /// <summary>
    /// Скрипт, отвечающий за воспроизведение звуков
    /// </summary>
    public SoundController soundController;

    /// <summary>
    /// Скрипт, отвечающий за управление здоровьем игрока
    /// </summary>
    private HealthManager healthManager;

    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    void Start()
    {
        SubscribeToHealthManager();
    }

    /// <summary>
    /// Метод, подписывающий необходимые классы на события HealthManager'а
    /// </summary>
    private void SubscribeToHealthManager()
    {
        healthManager = GetComponent<HealthManager>();

        List<IDamageObserver> damageObservers = new List<IDamageObserver>
        {
            healthDisplay,
            playerAnimationController,
            soundController
        };

        List<IHealObserver> healObservers = new List<IHealObserver>
        {
            healthDisplay,
            soundController
        };

        if (healthManager != null)
        {
            healthManager.Initialize(6, damageObservers, healObservers);
        }
    }
}
