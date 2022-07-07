using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 nextPoint = new Vector2(0f, 0f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        getNextPoint();
    }

    void FixedUpdate()
    {
        move();

        if (Input.GetMouseButton(0))
        {
            show(true);
        }
        else if (!Input.GetMouseButton(0))
        {
            show(false);
        }
    }

    #region player actions

    void move()
    {
        rb.MovePosition(nextPoint);
    }

    void show(bool show)
    {
        GetComponent<Renderer>().enabled = show;
    }

    #endregion

    #region player helper functions

    void getNextPoint()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        nextPoint = Vector2.Lerp(transform.position, mousePosition, 0.5f);
    }

    #endregion
}
