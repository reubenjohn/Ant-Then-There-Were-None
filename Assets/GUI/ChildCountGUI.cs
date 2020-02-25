using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildCountGUI : MonoBehaviour
{
    public Transform parent;

    private Text population;

    void Start() => population = GetComponent<Text>();

    void Update() => population.text = parent.childCount.ToString();
}
