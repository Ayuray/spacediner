using System.Collections.Generic;
using UnityEngine;

public class WaitingQueue
{
    private List<Vector2> slots;
    private readonly List<GameObject> aliens = new();
 
    public WaitingQueue(List<Vector2> slotPositions)
    {
        slots = slotPositions;
    }

    public int capacity => slots.Count;
    public int alienCount => aliens.Count;
    public bool queueFull => alienCount >= capacity;

    public bool tryEnqueue(GameObject alienGO)
    {
        if (queueFull || alienGO == null) return false;

        aliens.Add(alienGO);
        Debug.Log("Enqueued new Alien");
        UpdatePositions();
        return true;
    }

    private void UpdatePositions()
    {
        for (int i = 0; i < aliens.Count; i++)
        {
            var rt = aliens[i].GetComponent<RectTransform>();
            if(rt != null)
            {
                rt.anchoredPosition = slots[i];
            }
        }
    }
}
