using System.Collections.Generic;
using UnityEngine;

public class SystemSimulation : MonoBehaviour
{
    [Header("Alle verfügbaren Ingredients")]
    public List<Ingredient> allIngredients;

    [Header("Alle verfügbaren Dishes")]
    public List<Dish> allDishes;

    [Header("Simulation Settings")]
    public int ingredientCount = 10;

    [SerializeField] private IngredientGenerator ingredientGenerator;
    [SerializeField] private DishGenerator dishGenerator;

    private List<Ingredient> generatedIngredients = new();
    private List<Dish> generatedDishes = new();

    private void Start()
    {
        RunSimulation();
    }

    private void Update()
    {
    }

    void RunSimulation()
    {
        Debug.Log("=== SIMULATION START ===");

        // 1️⃣ Random Ingredients generieren
        

        for (int i = 0; i < 10; i++)
        {
            Ingredient ingredient = ingredientGenerator.GenerateIngredient();
            generatedIngredients.Add(ingredient);
        }

        Debug.Log("Generated Ingredients:");
        foreach (var ing in generatedIngredients)
        {
            Debug.Log("- " + ing.ingredientName);
        }

        // 2️⃣ Random Dish wählen
        for (int i = 0; i < 10; i++)
        {
            Dish dish = dishGenerator.GenerateDish();
            generatedDishes.Add(dish);
        }

        Debug.Log("Generated Dishes:");
        foreach (var d in generatedDishes)
        {
            Debug.Log("- " + d.dishName);
        }

        Debug.Log("Dish Requirements:");
        foreach (var d in generatedDishes)
        {
            Debug.Log($"Dish: {d.dishName}");

            foreach (var req in d.requirements)
            {
                if (req == null || req.requiredIngredient == null)
                {
                    Debug.LogWarning("Requirement er requiredIngredient is null.");
                    continue;
                }

                /*Debug.Log(
                    "Required Ingredients: " + req.requiredIngredient.ingredientName
                );*/
            }
        }

        Debug.Log("=== SIMULATION END ===");
    }
}