using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Конфиг, хранящий рецепты для крафта предметов
/// </summary>

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Recipes/Recipe")]
public class Recipe : ScriptableObject
{
    /// <summary>
    /// Список ингридиентов и их количества для крафта предмета
    /// </summary>
    [SerializeField] public List<RequiredItems> RequiredItems;

    /// <summary>
    /// Итоговый предмет
    /// </summary>
    [SerializeField] public GameObject ResultItem;

    /// <summary>
    /// Метод, проверяющий может ли скрафтиться предмет на основе текщуего инвентаря
    /// </summary>
    /// <param name="inventory">Инвентарь</param>
    /// <returns>True - если предмет может быть создан, иначе - false</returns>
    public bool CanCraft(Dictionary<PickableItems, int> inventory)
    {
        foreach (RequiredItems required in RequiredItems)
        {
            PickableItems item = required.Item;
            int requiredAmount = required.Amount;
            if (inventory.TryGetValue(item, out int amount))
            {
                if (amount < requiredAmount) 
                {
                    return false;
                }
            }
            else 
            {
                return false;
            }
        }
        return true;
    }
}