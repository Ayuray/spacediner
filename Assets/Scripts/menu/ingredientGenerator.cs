using System.Collections.Generic;
using UnityEngine;

public class IngredientGenerator : MonoBehaviour
{
    public List<Ingredient> allIngredients;

    private void Start()
    {
        allIngredients = new List<Ingredient>(Resources.LoadAll<Ingredient>("Menu/Ingredients"));
    }

    public Ingredient GenerateIngredient()
    {
        if (allIngredients == null || allIngredients.Count == 0) return null;

        return WeightedPicker.Pick(allIngredients, x => x.spawnWeight);
    }
}