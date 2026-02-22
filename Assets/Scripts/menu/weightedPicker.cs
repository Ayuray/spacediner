using System;
using System.Collections.Generic;
using UnityEngine;

public static class WeightedPicker
{
    public static T Pick<T>(IReadOnlyList<T> items, Func<T, float> getWeight)
    {
        int n_items = items.Count;
        if (items == null || n_items == 0)
        {
            Debug.Log("items == null");
            return default;
        }

        float total = 0f;
        for (int i = 0; i < n_items; i++)
        {
            var it = items[i];
            if (it == null) continue;
            total += Mathf.Max(0f, getWeight(it));
        }
        if (total <= 0f) return default;

        float r = UnityEngine.Random.value * total;
        float running = 0f;

        for (int i = 0; i < n_items; i++)
        {
            var it = items[i];
            if (it == null) continue;

            running += Mathf.Max(0f, getWeight(it));
            if (r <= running) {
                Debug.Log("Returned Item");
                return it;
            }
        }
        return items[items.Count - 1];  // should never happen
    }
}