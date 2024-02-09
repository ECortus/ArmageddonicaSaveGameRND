using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

public class CameraController : MonoBehaviour, ISave
{
    public string SaveID => "cam-pos";
    
    [SerializeField] private float speed;

    [Space] 
    [SerializeField] private Vector2 limitX;
    [SerializeField] private Vector2 limitZ;

    private Vector3 ClampPosition(Vector3 pos)
    {
        pos = new Vector3(
            Mathf.Clamp(pos.x, limitX.x, limitX.y),
            pos.y,
            Mathf.Clamp(pos.z, limitZ.x, limitZ.y)
            );
        
        return pos;
    }
    
    private Controls _controls;

    private void Awake()
    {
        _controls = new Controls();
        _controls.Enable();
    }

    private void OnDestroy()
    {
        _controls.Disable();
    }
    
    public object SaveToData()
    {
        Vector3 position = transform.position;
        return new Dictionary<string, float>
        {
            {"x", position.x},
            {"y", position.y},
            {"z", position.z}
        };
    }

    public void LoadFromData(SaveData save)
    {
        object dictionaryObject = save.GameData[SaveID];
        Dictionary<string, float> dictionary = ToDictionary(dictionaryObject);
        
        Vector3 position = new Vector3(
            dictionary["x"],
            dictionary["y"],
            dictionary["z"]);
        
        transform.position = position;
    }

    void Update()
    {
        var input = _controls.Camera.Movement.ReadValue<Vector2>();

        if (input != Vector2.zero)
        {
            var angle = transform.eulerAngles.y;
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, angle, 0));
            
            Vector3 direction = matrix.MultiplyPoint3x4(new Vector3(input.x, 0, input.y));
            Vector3 current = transform.position;
            Vector3 position = current + direction;

            transform.position = Vector3.Slerp(current, ClampPosition(position), speed * Time.deltaTime);
        }
    }
    
    private Dictionary<string, float> ToDictionary(object source)
    {
        var json = JsonConvert.SerializeObject(source);
        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, float>>(json);   
        return dictionary;
    }
}
