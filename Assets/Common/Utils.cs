using System;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static float Lerp(float a, float b, float t) => a + (b - a) * Mathf.Clamp(t, 0, 1);

    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        foreach (var item in enumerable)
            action.Invoke(item);
    }
    public static bool Contains(this LayerMask layerMask, int layerIndex) => layerMask == (layerMask | (1 << layerIndex));
}