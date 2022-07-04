using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 mousePosition;
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
        else
            showPlayer(false);
    }

    #region player actions

    void movePlayer()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        position = Vector2.Lerp(transform.position, mousePosition, 1.0f);
    }

    void showPlayer(bool show)
    {
        GetComponent<Renderer>().enabled = show;
    }

    #endregion
}
