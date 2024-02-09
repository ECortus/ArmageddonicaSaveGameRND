using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseBuilding : MonoBehaviour
{
    private int Height;
    private int ColorIndex;

    [SerializeField] private int maxHeight = 10;

    [Space] 
    [SerializeField] private Stage newStagePrefab;
    [SerializeField] private float spaceBetweenStages;

    private List<MeshRenderer> _meshes = new List<MeshRenderer>();

    public void Init(Vector3 position, int height, int colorIndex)
    {
        transform.position = position;
        
        Height = 0;
        ColorIndex = colorIndex;

        for (int i = 0; i < height; i++)
        {
            AddNewStage();
        }
    }

    public (Vector3, int, int) GetData()
    {
        return (transform.position, Height, ColorIndex);
    }

    public void AddNewStage()
    {
        if (Height < maxHeight)
        {
            float y = spaceBetweenStages * Height;
            Stage stage = Instantiate(newStagePrefab, transform.position + new Vector3(0, y, 0), Quaternion.identity, transform);

            var mesh = stage.GetComponentInChildren<MeshRenderer>();
            _meshes.Add(mesh);

            mesh.material = ColorManager.Instance.GetColorByIndex(ColorIndex);
            
            Height++;
        }
    }

    public void ChangeColor()
    {
        var pair = ColorManager.Instance.GetColorNextByIndex(ColorIndex);

        Material color = pair.Item1;
        ColorIndex = pair.Item2;
        
        for (int i = 0; i < _meshes.Count; i++)
        {
            _meshes[i].material = color;
        }
    }
}
