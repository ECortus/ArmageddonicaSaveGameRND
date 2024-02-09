using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class ColorManager : MonoBehaviour
{
    [Inject] public static ColorManager Instance { get; private set; }
    
    [SerializeField] private Material[] materials;

    [Inject] private void Awake()
    {
        Instance = this;
    }

    public int GetIndexOfColor(Material color)
    {
        return materials.IndexOf(color);
    }

    public Material GetColorByIndex(int index)
    {
        return materials[index];
    }

    public (Material, int) GetColorNextByIndex(int index)
    {
        int next = (index + 1) % materials.Length;
        return (materials[next], next);
    }
}
