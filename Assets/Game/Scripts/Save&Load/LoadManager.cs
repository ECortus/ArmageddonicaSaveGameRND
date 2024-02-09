using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Zenject;
using Newtonsoft.Json;
using File = System.IO.File;

public class LoadManager : MonoBehaviour
{
    [Inject] public static LoadManager Instance { get; private set; }
    
    [Inject] private void Awake()
    {
        Instance = this;
    }

    private string PrefsKey => "Save00";
    private string SAVE_FOLDER => Application.dataPath + "/Saves";

    public void LoadGame()
    {
        SaveData save = DeserializeData();
        if (save == null) return;

        var saveObjects = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>().OfType<ISave>();
        foreach (var VARIABLE in saveObjects)
        {
            VARIABLE.LoadFromData(save);
        }
    }
    
    SaveData DeserializeData()
    {
        // DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        // FileInfo[] files = directoryInfo.GetFiles();
        // FileInfo mostRecentFile = null;
        //
        // foreach (FileInfo file in files)
        // {
        //     if (mostRecentFile == null || file.LastWriteTime > mostRecentFile.LastWriteTime)
        //     {
        //         mostRecentFile = file;
        //     }
        // }
        //
        // if (mostRecentFile != null)
        // {
        //     var json = File.ReadAllText(mostRecentFile.FullName);
        //     var saveData = JsonConvert.DeserializeObject<SaveData>(json);
        //
        //     return saveData;
        // }

        if (PlayerPrefs.HasKey(PrefsKey))
        {
            var json = PlayerPrefs.GetString(PrefsKey);
            var saveData = JsonConvert.DeserializeObject<SaveData>(json);

            return saveData;
        }

        return null;
    }
}
