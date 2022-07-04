using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 position = new Vector2(0f, 0f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movePlayer();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            showPlayer(true);
            rb.MovePosition(position);
        }
        else if (!Input.GetMouseButton(0))
            showPlayer(false);
    }

    #region player actions

    void movePlayer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position = Vector2.Lerp(transform.position, mousePosition, 0.5f);
    }

    void showPlayer(bool show)
    {
        GetComponent<Renderer>().enabled = show;
    }

    #endregion
}
