using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    void Start()
    {
        LoadManager.Instance.LoadGame();
    }
}
