using UnityEngine;

/// <summary>
/// Класс, представляющий объект: "витамины"
/// </summary>
public class MedicineUse : MonoBehaviour, IUseScript
{
    /// <summary>
    /// Скрипт, отвечающий за здоровье игрока
    /// </summary>
    private HealHandler healHandler;

    /// <summary>
    /// Количество очков, на которое аптечка восполняет здоровье
    /// </summary>
    private int healPoints = 2;

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
    /// Метод использования витаминов
    /// </summary>
    /// <returns>True - если витамины были успешно выпиты, иначе - false</returns>
    public bool Use()
    {
        if (healHandler.Heal(healPoints))
        {
            SoundController.Instance.PlaySound(SoundType.Medicine, SoundController.Instance.inventoryAudioSource);
            return true;
        }

        return false;
    }
}
