using UnityEngine;
using static HelpPhrasesModule;

/// <summary>
/// Класс, отвечающий за взаимодействие с объектом: "нож"
/// </summary>
public class Knife : MonoBehaviour, IInteractable
{
    private string helpPhrase = actionToPhrase[Action.PickUp];

    /// <summary>
    /// Метод, обеспечивающий взаимодействие с объектом
    /// </summary>
    public void Interact()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Метод, предоставляющий подсказку для объекта
    /// </summary>
    /// <returns>Подсказка для объекта</returns>

    public string GetHelpPhrase()
    {
        return helpPhrase;
    }
}
