using UnityEngine;

/// <summary>
/// Класс, представляющий объект: "кола"
/// </summary>
public class CokeUse : MonoBehaviour, IUseScript
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
    /// Метод использования колы
    /// </summary>
    /// <returns>True - если кола была успешно выпита, иначе - false</returns>
    public bool Use()
    {
        if (healHandler.Heal(healPoints))
        {
            SoundController.Instance.PlaySound(SoundType.Coke, SoundController.Instance.inventoryAudioSource);
            return true;
        }

        return false;
    }
}
