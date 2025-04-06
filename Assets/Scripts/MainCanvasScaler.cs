using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasScaler : MonoBehaviour
{

    [SerializeField] private CanvasScaler _canvasScaler;
    
    private void Awake()
    {
        _canvasScaler.scaleFactor = Screen.width / 1920f;
    }
}