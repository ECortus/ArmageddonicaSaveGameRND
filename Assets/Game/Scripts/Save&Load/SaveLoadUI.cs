using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadUI : MonoBehaviour
{
    [SerializeField] private Button saveButton, loadButton;

    void Start()
    {
        saveButton.onClick.AddListener(SaveManager.Instance.SaveGame);
        loadButton.onClick.AddListener(LoadManager.Instance.LoadGame);
    }
}
