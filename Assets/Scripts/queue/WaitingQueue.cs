using System;
using System.Collections.Generic;
using UnityEngine;

public class WaitingQueue
{
    private bool orderStarted = false;

    private List<Vector2> slots;
    private readonly List<GameObject> aliens = new();

    public static Vector2 FirstSlotPosition { get; private set; }

    public WaitingQueue(List<Vector2> slotPositions)
    {
        slots = slotPositions;
        FirstSlotPosition = slots[0];
    }

    public static event Action OnOrderStart;

    public int capacity => slots.Count;
    public int alienCount => aliens.Count;
    public bool queueFull => alienCount >= capacity;

    public bool TryAddQueue(GameObject alienGO)
    {
        if (queueFull || alienGO == null) return false;

        aliens.Add(alienGO);
        UpdatePositions();
        return true;
    }

    public bool TryRemoveQueue()
    {
        if (alienCount > 0)
        {
            GameObject front = aliens[0];
            aliens.RemoveAt(0);

            // remove alien from rightmost position
            var mover = front.GetComponent<QueueAlienLerpMover>();
            if (mover == null) mover = front.AddComponent<QueueAlienLerpMover>();

            Vector2 exitPos = slots[0] + new Vector2(150f, 0f);
            mover.ExitTo(exitPos);

            //UpdatePositions();
            orderStarted = false;
            return true;
        }
        return false;
    }

    private void UpdatePositions()
    {
        if (alienCount == 0) return;

        for (int i = 0; i < alienCount; i++)
        {
            var go = aliens[i];
            if (go == null)
            {
                if (go == aliens[0])
                {
                    orderStarted = false;
                }
                continue;
            }

            var mover = go.GetComponent<QueueAlienLerpMover>();
            if (mover == null)
                mover = go.AddComponent<QueueAlienLerpMover>();

            if (i == 0)
            {
                if (!orderStarted)
                {
                    OnOrderStart?.Invoke();
                    orderStarted = true;
                }

                mover.MoveTo(slots[i]);
            }
            else
            {
                mover.MoveTo(slots[i]);
            }
        }
    }
}
