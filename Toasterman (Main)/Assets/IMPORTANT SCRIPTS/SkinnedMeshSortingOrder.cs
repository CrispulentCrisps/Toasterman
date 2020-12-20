using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedMeshSortingOrder : MonoBehaviour
{

    public string layerName;
    public int order;

    private SkinnedMeshRenderer rend;

    void Awake()
    {
        rend = GetComponent<SkinnedMeshRenderer>();
        rend.sortingLayerName = layerName;
        rend.sortingOrder = order;
    }

    public void Update()
    {
        if (rend.sortingLayerName != layerName)
            rend.sortingLayerName = layerName;
        if (rend.sortingOrder != order)
            rend.sortingOrder = order;
    }

    public void OnValidate()
    {
        rend = GetComponent<SkinnedMeshRenderer>();
        rend.sortingLayerName = layerName;
        rend.sortingOrder = order;
    }
}