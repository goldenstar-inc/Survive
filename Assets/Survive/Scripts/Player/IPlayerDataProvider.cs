using System;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Интерфейс, содержащий ссылки на компоненты игрока
/// </summary>
public abstract class PlayerDataProvider : MonoBehaviour
{
    public abstract PlayerSetting PlayerSetting { get; }
    public abstract HealthManager HealthManager { get; }
    public abstract AmmoHandler AmmoHandler { get; }
    public abstract MoneyHandler MoneyHandler { get; }
    public abstract SoundController SoundController { get; }
}
