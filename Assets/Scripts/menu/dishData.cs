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
    public Vector2Int cell;
    public Ingredient requiredIngredient;
}