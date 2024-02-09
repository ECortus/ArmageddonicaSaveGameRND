using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stage : MonoBehaviour
{
    private BaseBuilding _base;
    
    private void Awake()
    {
        _base = GetComponentInParent<BaseBuilding>();
    }

    public void AddNewStage()
    {
        _base.AddNewStage();
    }

    public void ChangeColor()
    {
        _base.ChangeColor();
    }
}
