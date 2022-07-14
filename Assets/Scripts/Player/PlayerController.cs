using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Rigidbody rb;
    Vector3 pathToMouse = new Vector3 (0f, 0f, 0f);

    #region UnityFunctions

    void Start() { rb = GetComponent<Rigidbody>(); }

    void Update() { getPathToMouse(); }

    void FixedUpdate()
    {
        move();

        if (Input.GetMouseButton(0)) { show(true); }
        else { show(false); }
    }

    #endregion

    #region PlayerActions

    void move() { rb.MovePosition(pathToMouse); }

    void show(bool show) { GetComponent<Renderer>().enabled = show; }

    void collect() {
        
    }
   
    #endregion

    #region HelperFunctions

    void getPathToMouse() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pathToMouse = Vector2.Lerp(transform.position, mousePosition, 0.5f);
    }

    #endregion
}