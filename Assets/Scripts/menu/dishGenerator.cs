using System.Collections.Generic;
using UnityEngine;

public class DishGenerator : MonoBehaviour
{
    public List<Dish> allDishes;

    private void Start()
    {
        allDishes = new List<Dish>(Resources.LoadAll<Dish>("Menu/Dishes"));
    }

    public Dish GenerateDish()
    {
        if (allDishes == null || allDishes.Count == 0) return null;

       return WeightedPicker.Pick(allDishes, x => x.pickWeight);
    }
}