using UnityEngine;

public class Wander : MonoBehaviour {
    Rigidbody2D rb; 
    [SerializeField] float speed;
    private float range = 0.25f;
    private Vector2 destination;

    void Start() { 
        rb = GetComponent<Rigidbody2D>();
        setDestination();
    }

    void Update() {
        if (Vector2.Distance(transform.position, destination) < range) { setDestination(); }
    }

    void FixedUpdate() {
        if (!Input.GetMouseButton(0)) { wander(); }
    }

    private void OnCollisionEnter2D (Collision2D collision) { 
        string axis = collision.contacts[0].normal.x != 0 
            ? "x" 
            : "y";

        float direction = collision.contacts[0].normal.x != 0 
            ? collision.contacts[0].normal.x 
            : collision.contacts[0].normal.y;

        destination = axis == "x" 
            ? new Vector2(direction, Random.Range(-10, 10))
            : new Vector2(Random.Range(-10, 10), direction);
    }

    private void setDestination() { 
        destination = new Vector2(Random.Range(-10, 10),Random.Range(-10, 10));
    }

    private void wander() { 
        rb.MovePosition(
            (Vector2)transform.position + destination.normalized * speed * Time.fixedDeltaTime
        ); 
    }
}