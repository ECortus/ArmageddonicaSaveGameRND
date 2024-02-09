using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Zenject;
using Newtonsoft.Json;
using File = System.IO.File;

public class SaveManager : MonoBehaviour
{
    [Inject] public static SaveManager Instance { get; private set; }
    
    [Inject] private void Awake()
    {
        Instance = this;
    }

    private string PrefsKey => "Save00";
    private string SAVE_FOLDER => Application.dataPath + "/Saves";

    public void SaveGame()
    {
        SaveData save = new SaveData();
        
        var saveObjects = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>().OfType<ISave>();
        foreach (var VARIABLE in saveObjects)
        {
            save.GameData.Add(VARIABLE.SaveID, VARIABLE.SaveToData());
        }
        
        SerializeData(save);
    }

    void SerializeData(SaveData data)
    {
        // int saveNumber = 0;
        // while (File.Exists(SAVE_FOLDER + $"/SaveData0{saveNumber.ToString()}.json"))
        // {
        //     saveNumber++;
        // }
        //
        // var jsonFile = JsonConvert.SerializeObject(data);
        // File.WriteAllTextAsync(SAVE_FOLDER + $"/SaveData0{saveNumber.ToString()}.json", jsonFile);
        
        var json = JsonConvert.SerializeObject(data);
        
        PlayerPrefs.SetString(PrefsKey, json);
        PlayerPrefs.Save();
    }
}
