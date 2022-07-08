using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyController : MonoBehaviour
{
    Rigidbody2D rb;

    public GameObject player;

    [SerializeField] float speed;
    private bool hasArrived = true;

    [SerializeField] bool showLineRenderer;
    public LineRenderer lineRenderer;
    private int numPoints = 1000;
    private Vector3[] points = new Vector3[1000];

    // TODO: 
    // - if player holds down, reset bevier curve from friend's new position
    // - make sure friend can collider with objs (dynamic/kinematic)
    // - make sure if friend collides with obj, recalc bezier curve

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() { }

    void FixedUpdate() {
        if (Input.GetMouseButton(0)) {
            if (Vector2.Distance(transform.position, player.transform.position) > 0.25f) { 
                follow();
                Debug.Log(Vector2.Distance(transform.position, player.transform.position) > 0.5f);
                Debug.Log(Vector2.Distance(transform.position, player.transform.position));
            }
        }

        else if (!Input.GetMouseButton(0) && hasArrived) {
            setBezierCurve();
            StartCoroutine(wander());
        }
    }

    #region friend actions

    void follow() {
        Vector2 nextPointFollow = Vector2.Lerp(transform.position, player.transform.position, 1 * Time.fixedDeltaTime);
        rb.MovePosition(nextPointFollow);
    }

    IEnumerator wander() {
        hasArrived = false;

        for (int i = 1; i < points.Length; i++) {
            if (Input.GetMouseButton(0)) break;
            Vector2 nextPointWander = Vector2.MoveTowards(
                points[i - 1], 
                points[i], 
                Vector2.Distance(points[i - 1], points[i])
            );
            rb.MovePosition(nextPointWander);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        hasArrived = true;
    }

    #endregion

    #region bezier curve functions

    // quadratic
    // private Vector3 getBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t) {
    //     return (((1 - t) * (1 - t)) * p0) + (2 * (1 - t) * t * p1) + (t * t * p2);
    // }

    // cubic
    private Vector3 getBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
        return (((1 - t) * (1 - t) * (1 - t)) * p0) + (3 * ((1 - t) * (1 - t)) * t * p1) + (3 * (1 - t) * t * t * p2) + (t * t * t * p3);
    }

    private void setBezierCurve() {
        GameObject p1 = new GameObject();
        p1.transform.position = new Vector3(Random.Range(-2, 2), Random.Range(-4, 4), 0);

        GameObject p2 = new GameObject();
        p2.transform.position = new Vector3(Random.Range(-2, 2), Random.Range(-4, 4), 0);

        GameObject p3 = new GameObject();
        p3.transform.position = new Vector3(Random.Range(-2, 2), Random.Range(-4, 4), 0);

        if (showLineRenderer) lineRenderer.positionCount = numPoints;

        for (int i = 1; i < numPoints + 1; i++) {
            float t = i / (float)numPoints;
            points[i - 1] = getBezierPoint(
                transform.position, 
                p1.transform.position, 
                p2.transform.position, 
                p3.transform.position, 
                t
            );
        }

        if (showLineRenderer) lineRenderer.SetPositions(points);

        Destroy(p1);
        Destroy(p2);
        Destroy(p3);
    }

    #endregion
}
