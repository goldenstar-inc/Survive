using System;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Интерфейс, содержащий ссылки на компоненты игрока
/// </summary>
public interface IPlayerDataProvider
{
    public HealthManager HealthManager { get; }
    public AmmoHandler AmmoHandler { get; }
    public MoneyHandler MoneyHandler { get; }
    public SoundController SoundController { get; }
}
