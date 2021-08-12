using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PressDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
public bool buttonPressed;
 
public void OnPointerDown(PointerEventData eventData){
     buttonPressed = true;
}
 
public void OnPointerUp(PointerEventData eventData){
    buttonPressed = false;
}
}
