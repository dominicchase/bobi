using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyController : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject player;
    Vector2 nextPoint = new Vector2(0f, 0f);
    [SerializeField] float speed;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() { 
        getNextPoint();
    }

    void FixedUpdate() {
        if (Input.GetMouseButton(0)) {
            if (Vector2.Distance(transform.position, player.transform.position) > 0.25f) { 
                follow();
            }
        }
    }

    #region friend actions

    void getNextPoint() {
        Vector2 nextPoint = Vector2.Lerp(transform.position, player.transform.position, Time.deltaTime);
    }

    void follow() {
        rb.MovePosition(nextPoint);
    }

    #endregion
}
