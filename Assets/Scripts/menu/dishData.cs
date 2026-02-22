using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDish", menuName = "Game/Dish")]
public class Dish : ScriptableObject
{
    public string dishName;
    public Sprite icon;
    public float pickWeight = 1f;

    // Musterlösung: welche Grid-Zelle muss welches Ingredient enthalten
    public List<CellRequirement> requirements = new List<CellRequirement>();
}

[System.Serializable]
public class CellRequirement
{
    public int score; // 1 = good, 0 = neutral, -1 = bad
    public ItemData requiredIngredient;
}