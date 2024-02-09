using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMouseController : MonoBehaviour
{
    private Camera _camera;

    private int _uiMask;
    private int _groundMask;
    private int _stageMask;

    private void Awake()
    {
        _camera = Camera.main;

        _uiMask = LayerMask.NameToLayer("UI");
        _groundMask = LayerMask.NameToLayer("Ground");
        _stageMask = LayerMask.NameToLayer("Stage");
    }

    void Update()
    {
        var lmb = Input.GetMouseButtonDown(0);
        var rmb = Input.GetMouseButtonDown(1);
        
        var getInput = lmb || rmb;
        if (getInput && !IsPointerOverUIElement())
        {
            var input = Input.mousePosition;
            var ray = _camera.ScreenPointToRay(input);
            
            if(Physics.Raycast(ray, out RaycastHit hitInfo, 999))
            {
                if (hitInfo.transform.gameObject.layer == _groundMask)
                {
                    var ground = hitInfo.transform.GetComponent<GroundPlane>();
                    if (ground)
                    {
                        if (lmb)
                        {
                            ground.SpawnNewBuilding(hitInfo.point);
                        }
                        else if (rmb)
                        {
                            ground.ChangeColor();
                        }
                    }
                }
                
                if (hitInfo.transform.gameObject.layer == _stageMask)
                {
                    var stage = hitInfo.transform.GetComponent<Stage>();
                    if (stage)
                    {
                        if (lmb)
                        {
                            stage.AddNewStage();
                        }
                        else if (rmb)
                        {
                            stage.ChangeColor();
                        }
                    }
                }
            }
        }
    }
    
    private bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }
    
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaycastResult = eventSystemRaysastResults[index];
            if (curRaycastResult.gameObject.layer == _uiMask)
            {
                return true;
            }
        }
        return false;
    }
    
    private List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        
        return raysastResults;
    }
}
