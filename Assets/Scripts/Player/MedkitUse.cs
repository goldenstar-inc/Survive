using UnityEngine;

/// <summary>
/// Класс, отвечающий за реализацию механики работы аптечки
/// </summary>
public class MedkitUse : MonoBehaviour, IUseScript
{
    /// <summary>
    /// Скрипт, отвечающий за здоровье игрока
    /// </summary>
    private HealHandler healHandler;

    /// <summary>
    /// Количество очков, на которое аптечка восполняет здоровье
    /// </summary>
    private int healPoints = 1;

    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    void Start()
    {
        healHandler = GetComponent<HealHandler>();

        if (healHandler == null)
        {
            Debug.LogWarning("HealHandler not loaded");
        }
    }

    /// <summary>
    /// Метод использования аптечки
    /// </summary>
    /// <returns>True - если аптечка была успешно использована, иначе - false</returns>
    public bool Use()
    {
        if (healHandler.Heal(healPoints))
        {
            SoundController.Instance.PlaySound(SoundType.BeingHealed, SoundController.Instance.inventoryAudioSource);
            return true;
        }

        return false;
    }
}
