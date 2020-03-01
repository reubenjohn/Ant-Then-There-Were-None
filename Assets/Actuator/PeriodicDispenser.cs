using System;
using UnityEngine;

public class PeriodicDispenser : Dispenser
{
    public float spawnFrequency = 1f;
    public int selectedBreadcrumb;

    private float lastDispenseTime = 0f;

    public void Update()
    {
        if (spawnFrequency > 0)
        {
            if (Time.time > lastDispenseTime + 1 / spawnFrequency)
            {
                Dispense(selectedBreadcrumb);
                lastDispenseTime = Time.time;
            }
        }
    }
}
