using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIngredient", menuName = "Game/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public Sprite icon;
    public float spawnWeight = 1f;
    public int height;
    public int width;

    // Basisform (Rotation 0°)
    public List<Vector2Int> shapeOffsets = new List<Vector2Int>()
    {
        Vector2Int.zero
    };

    // Gibt Offsets abhängig von Rotation zurück (0,1,2,3)
    public List<Vector2Int> GetRotatedOffsets(int rotation)
    {
        rotation = ((rotation % 4) + 4) % 4;

        List<Vector2Int> result = new List<Vector2Int>();

        foreach (var cell in shapeOffsets)
        {
            result.Add(Rotate(cell, rotation));
        }

        return Normalize(result);
    }

    private Vector2Int Rotate(Vector2Int cell, int rotation)
    {
        switch (rotation)
        {
            case 0: return cell;
            case 1: return new Vector2Int(-cell.y, cell.x);
            case 2: return new Vector2Int(-cell.x, -cell.y);
            case 3: return new Vector2Int(cell.y, -cell.x);
        }

        return cell;
    }

    // Sorgt dafür, dass die kleinste X/Y wieder bei 0 startet
    private List<Vector2Int> Normalize(List<Vector2Int> cells)
    {
        int minX = int.MaxValue;
        int minY = int.MaxValue;

        foreach (var c in cells)
        {
            if (c.x < minX) minX = c.x;
            if (c.y < minY) minY = c.y;
        }

        List<Vector2Int> normalized = new List<Vector2Int>();
        foreach (var c in cells)
        {
            normalized.Add(new Vector2Int(c.x - minX, c.y - minY));
        }

        return normalized;
    }
}