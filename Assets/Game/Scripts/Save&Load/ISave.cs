using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISave
{
    public string SaveID { get; }
    public object SaveToData();
    public void LoadFromData(SaveData data);
}
