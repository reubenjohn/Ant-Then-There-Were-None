using UnityEditorExtensions;
using UnityEngine;

namespace UnityEditorExtensionsDemo
{
    public class Demo : MonoBehaviour
    {
        [Tag] public string tagAttribute;
        [Layer] public int layerAttribute;
        [SerializeField, Tag] private string privateSerializedTagAttribute;
        [SerializeField, Layer] private int privateSerializedLayerAttribute;
    }
}