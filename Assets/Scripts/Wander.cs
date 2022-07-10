using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour {
    
    [SerializeField] float moveSpeed;
    [SerializeField] float rotSpeed;
    private float range = 0.5f;
    private Vector2 destination;

    void Start() { 
        setDestination();
    }

    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, destination) < range) { 
            // new WaitForSecondsRealtime(Random.Range(0, 5));
            setDestination(); 
        }
    }

    void FixedUpdate() {
        // if (!Input.GetMouseButton(0)) { wander(); }
    }

    private void OnCollisionEnter2D (Collision2D collision) { }

    private void setDestination() { 
        destination = new Vector2(Random.Range(-2.5f, 2.5f), Random.Range(-4.5f, 4.5f));
        // RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

    }

    private void wander() { 
        // rb.MovePosition(
        //     (Vector2)transform.position + destination * speed * Time.fixedDeltaTime
        // ); 
    }
}