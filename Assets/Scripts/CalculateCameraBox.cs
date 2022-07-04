using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateCameraBox : MonoBehaviour
{
    private Camera mainCamera;
    private BoxCollider2D cameraBox;
    private float sizeX,
        sizeY,
        ratio;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        cameraBox = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        sizeY = mainCamera.orthographicSize * 2;
        ratio = (float)Screen.width / (float)Screen.height;
        sizeX = sizeY * ratio;
        cameraBox.size = new Vector2(sizeX, sizeY);
    }
}
