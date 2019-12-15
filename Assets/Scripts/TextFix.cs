using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TextFix : MonoBehaviour
{

    [SerializeField]
    public string layer;

    void Start()
    {
        GetComponent<MeshRenderer>().sortingLayerID = SortingLayer.NameToID(layer);
        GetComponent<MeshRenderer>().sortingOrder = 1000;
    }
}
