using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour, ISave
{
    public string SaveID => "timer-total";
    
    private float Amount { get; set; }
    
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public object SaveToData()
    {
        return Amount;
    }

    public void LoadFromData(SaveData save)
    {
        object total = save.GameData[SaveID];

        Amount = Convert.ToSingle(total);
        SetText();
    }
    
    void Update()
    {
        Amount += Time.deltaTime;
        SetText();
    }

    void SetText()
    {
        string minutes, seconds;
        
        minutes = ((int)(Amount / 60)).ToString();
        minutes = (minutes.Length < 2 ? "0" : "") + minutes;
        
        seconds = ((int)(Amount % 60)).ToString();
        seconds = (seconds.Length < 2 ? "0" : "") + seconds;

        text.text = $"{minutes}:{seconds}";
    }
}
