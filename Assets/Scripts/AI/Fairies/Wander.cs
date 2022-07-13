using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour {
    public GameObject go;
    Rigidbody rb;
    private BezierCurve bezierCurve;

    Animator anim;
    private static readonly int Idle = Animator.StringToHash("Idle");

    public LineRenderer lineRenderer;
    [SerializeField] bool showLineRenderer;
    
    // [SerializeField] float speed;
    private bool wandering = false;

    private Vector3[] points;

    void Start() { 
        rb = go.GetComponent<Rigidbody>(); 
        bezierCurve = gameObject.GetComponent<BezierCurve>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update() { }

    void FixedUpdate() { 
        if (!wandering) {
            points = bezierCurve.getBezierCurve(bezierCurve.getBezierPoints()).ToArray();
            if (showLineRenderer) { renderLine(); }
            StartCoroutine(wander()); 
        }
    }

    IEnumerator wander() {
        wandering = true;

        for (int i = 1; i < points.Length; i++) {
            Vector2 nextPoint = Vector2.Lerp(points[i - 1], points[i], Time.fixedDeltaTime);
            rb.MovePosition(nextPoint);

            yield return new WaitForFixedUpdate();
        }

        anim.CrossFade("Idle", 0.5f, 0);
        yield return new WaitForSecondsRealtime(Random.Range(1, 4));
        wandering  = false;
    }

    private void renderLine() { 
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }
}