using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class SaveData
{
    public Dictionary<string, object> GameData = new Dictionary<string, object>();
}
