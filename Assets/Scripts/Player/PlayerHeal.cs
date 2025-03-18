using UnityEngine;

/// <summary>
/// Класс, ответственный за восстановление очков здоровья игрока
/// </summary>
public class PlayerHeal : MonoBehaviour
{
    private HealHandler healHandler;
    private int healPoints = 1;
    
    /// <summary>
    /// Метод, вызывающийся при старте объекта
    /// </summary>
    void Start()
    {
        healHandler = GetComponent<HealHandler>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && healHandler != null)
        {
            healHandler.Heal(healPoints);
        }
    }
}
