using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

public class BuildingManager : MonoBehaviour, ISave
{
    public string SaveID => "building-manager-building-list";
    
    [Inject] public static BuildingManager Instance { get; private set; }

    [SerializeField] private BaseBuilding baseBuildingPrefab;

    private List<BaseBuilding> Buildings = new List<BaseBuilding>();

    [Inject] private void Awake()
    {
        Instance = this;
    }

    [Serializable]
    public struct BaseBuildingSaveData
    {
        public Dictionary<string, float> Position;
        public int Height;
        public int ColorIndex;
    }

    public object SaveToData()
    {
        List<BaseBuildingSaveData> data = new List<BaseBuildingSaveData>();
        
        (Vector3, int, int) building;
        for (int i = 0; i < Buildings.Count; i++)
        {
            building = Buildings[i].GetData();
            data.Add(new BaseBuildingSaveData
            {
                Position = new Dictionary<string, float>()
                {
                    {"x", building.Item1.x},
                    {"y", building.Item1.y},
                    {"z", building.Item1.z}
                },
                Height = building.Item2,
                ColorIndex = building.Item3
            });
        }
        
        return data;
    }

    public void LoadFromData(SaveData save)
    {
        for (int i = 0; i < Buildings.Count; i++)
        {
            Destroy(Buildings[i].gameObject);
        }
        Buildings.Clear();

        object dataObject = save.GameData[SaveID];
        List<BaseBuildingSaveData> data = ToList(dataObject);

        BaseBuildingSaveData buildData;
        Vector3 position;
        
        for (int i = 0; i < data.Count; i++)
        {
            buildData = data[i];
            position = new Vector3(
                buildData.Position["x"],
                buildData.Position["y"],
                buildData.Position["z"]);
            
            SpawnNewBuilding(position, buildData.Height, buildData.ColorIndex);
        }
    }

    public void SpawnNewBuilding(Vector3 point, int height = 1, int color = 0)
    {
        BaseBuilding build = Instantiate(baseBuildingPrefab, transform);
        build.Init(point, height, color);
        
        Buildings.Add(build);
    }
    
    private List<BaseBuildingSaveData> ToList(object source)
    {
        var json = JsonConvert.SerializeObject(source);
        var list = JsonConvert.DeserializeObject<List<BaseBuildingSaveData>>(json);   
        return list;
    }
}
