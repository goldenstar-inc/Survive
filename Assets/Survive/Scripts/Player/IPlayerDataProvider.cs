using System;
using Unity.VisualScripting;

/// <summary>
/// Интерфейс, содержащий ссылки на компоненты игрока
/// </summary>
public interface IPlayerDataProvider
{
    public AmmoHandler ammoHandler { get; }
}
