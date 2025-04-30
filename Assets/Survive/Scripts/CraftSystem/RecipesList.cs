using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipesList", menuName = "Recipes/RecipesList")]
public class RecipesList : ScriptableObject
{
    [SerializeField] public List<Recipe> Recipes;

}
