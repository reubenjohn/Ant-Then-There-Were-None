using System;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    public GameObject[] breadcrumbs;
    public Transform parent = null;
    public Transform spawnPoint = null;

    public void Dispense(int selectedBreadcrumb)
    {
        Instantiate(breadcrumbs[selectedBreadcrumb], spawnPoint.position, spawnPoint.rotation, parent);
    }
}
