using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Camera mainCamera;

    void Start() { }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            player.SetActive(true);
            movePlayer();
        }
        else if (!Input.GetMouseButtonUp(0))
            player.SetActive(false);
    }

    void movePlayer()
    {
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0f;
        transform.position = worldPosition;
    }
}
