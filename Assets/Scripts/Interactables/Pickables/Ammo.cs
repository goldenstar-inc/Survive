using UnityEngine;
using static HelpPhrasesModule;

/// <summary>
/// Класс, представляющий объект: "Патроны"
/// </summary>
public class Ammo : MonoBehaviour, IInteractable, IAmountable
{
    private AmmoHandler ammoHandler;
    
    /// <summary>
    /// Свойство, хранящее подсказку для игрока
    /// </summary>
    public string helpPhrase => actionToPhrase[Action.PickUp];

    /// <summary>
    /// Количество патронов
    /// </summary>
    public int quanity;

    /// <summary>
    /// Свойство, возвращающее количество патронов
    /// </summary>
    public int Quanity 
    {
        get => quanity;
        set => quanity = value;
    }

    private void Start()
    {
        ammoHandler = FindAnyObjectByType<AmmoHandler>();
        if (ammoHandler == null)
        {
            Debug.LogError("AmmoHandler not found in the scene!");
        }
    }

    /// <summary>
    /// Метод, обеспечивающий взаимодействие с объектом
    /// </summary>
    public void Interact()
    {
        Destroy(gameObject);
        ammoHandler.CollectAmmo(quanity);
    }
}
