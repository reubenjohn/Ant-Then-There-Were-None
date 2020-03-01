using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntEnvironment : MonoBehaviour
{
    [SerializeField] private Transform markersTransform = null;
    public Transform MarkersTransform { get => markersTransform; }
}
