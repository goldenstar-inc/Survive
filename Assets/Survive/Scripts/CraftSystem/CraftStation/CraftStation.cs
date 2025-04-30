using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftStation : MonoBehaviour, IInteractable
{
    [SerializeField] private IntreractableData data;
    [SerializeField] private RecipesList crafts; 
    [SerializeField] private Transform dropPoint;
    public IntreractableData Data => data;
    public event Action OnInteract;

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
