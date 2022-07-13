using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyController : MonoBehaviour
{
    // Rigidbody2D rb;
    // public GameObject player;
    // Vector2 nextPoint = new Vector2(0f, 0f);

    // Vector2 destination;
    [SerializeField] float speed;

    void Start() { 
        if (!Input.GetMouseButton(0))
            wander();
    }

    void Update() { 
        
    }

    void FixedUpdate() { }

    Vector2 getDestination() {
        Vector2 destination = new Vector2(Random.Range(-2, 2),Random.Range(-4, 4));
        return destination;
    }

    void wander() {
        Vector3 destination = getDestination();

        if (Vector2.Distance(transform.position, destination) < 0.5)
            wander();

        else 
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }

    #region friend actions

    #endregion
}


// if (Input.GetMouseButton(0)) {
//     follow();
//     // if (Vector2.Distance(transform.position, player.transform.position) > 0.25f) { 
//     // }
// }

// Vector2 nextPoint = Vector2.Lerp(transform.position, player.transform.position, speed * Time.deltaTime);

// rb.MovePosition(nextPoint);