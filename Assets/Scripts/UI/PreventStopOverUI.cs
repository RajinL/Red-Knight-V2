using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//https://docs.unity3d.com/2018.3/Documentation/ScriptReference/EventSystems.IPointerClickHandler.html
public class PreventStopOverUI : MonoBehaviour, IPointerClickHandler
{

    // Detect if the mouse cursor clicks an object - currently used if there's a mouse click ovr the Menu button
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GameManager.instance.StopPlayerInput();
        //GameManager.instance.acceptPlayerInput = false;
    }
}
