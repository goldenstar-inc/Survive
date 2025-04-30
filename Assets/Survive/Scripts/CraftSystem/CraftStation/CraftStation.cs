using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, представляющий место крафта
/// </summary>
public class CraftStation : MonoBehaviour, IInteractable
{
    [SerializeField] private IntreractableData data;
    [SerializeField] private RecipesList crafts; 
    [SerializeField] private Transform dropPoint;
    public IntreractableData Data => data;
    public event Action OnInteract;

    /// <summary>
    /// Метод взаимодействия
    /// </summary>
    /// <param name="interactor">Данные игрока, который взаимодействует</param>
    /// <returns>true - если взаимодействие прошло успешно, иначе - false</returns>
    public bool Interact(PlayerDataProvider interactor)
    {
        if (interactor != null && interactor is IInventoryProvider inventoryProvider)
        {
            Inventory inventory = inventoryProvider.Inventory;
            Dictionary<PickableItems, int> itemToCount = inventory.GetItemToCountMap();
            List<Recipe> availableRecipes = GetAvailableRecipes(itemToCount);
            if (availableRecipes.Count > 0)
            {
                GameObject resultItem = availableRecipes[0].ResultItem;
                Instantiate(resultItem, dropPoint.position, Quaternion.identity);
                inventory.RemoveItemsByRecipe(availableRecipes[0]);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Получение доступных рецептов
    /// </summary>
    /// <param name="itemToCount">Словарь предмет-его количество в инвентаре</param>
    /// <returns>Доступные рецепты</returns>
    private List<Recipe> GetAvailableRecipes(Dictionary<PickableItems, int> itemToCount)
    {
        List<Recipe> availableRecipes = new ();

        foreach (Recipe recipe in crafts.Recipes)
        {
            if (recipe.CanCraft(itemToCount))
            {
                availableRecipes.Add(recipe);
            }
        }

        return availableRecipes;
    }
}
