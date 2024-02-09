using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlane : MonoBehaviour, ISave
{
    public string SaveID => "ground-color-index";
    
    [SerializeField] private int defaultColorIndex = 3;
    private int ColorIndex = -1;

    private MeshRenderer _mesh;

    private void Awake()
    {
        _mesh = GetComponentInChildren<MeshRenderer>();

        if (ColorIndex == -1)
        {
            ColorIndex = defaultColorIndex;
        }
    }
    
    public object SaveToData()
    {
        return ColorIndex;
    }

    public void LoadFromData(SaveData save)
    {
        object index = save.GameData[SaveID];

        ColorIndex = Convert.ToInt32(index);
        _mesh.material = ColorManager.Instance.GetColorByIndex(ColorIndex);
    }

    public void SpawnNewBuilding(Vector3 point)
    {
        BuildingManager.Instance.SpawnNewBuilding(point);
    }

    public void ChangeColor()
    {
        var pair = ColorManager.Instance.GetColorNextByIndex(ColorIndex);
        
        _mesh.material = pair.Item1;
        ColorIndex = pair.Item2;
    }
}
