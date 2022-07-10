using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BezierCurve : MonoBehaviour {

    public LineRenderer lineRenderer;
    [SerializeField] bool showLineRenderer = true;
    [SerializeField] AnimationCurve curve;

    [SerializeField] int numPoints;
    [SerializeField] float spacing;
    private Vector3[] smoothPoints;
    private int smoothIdx = 1;

    Rigidbody2D rb;
    public GameObject tmp;

    void Start() {
        rb = tmp.GetComponent<Rigidbody2D>();
        // get first curve
        smoothPoints = getCurve(getBezierPoints(), spacing).ToArray();
        StartCoroutine(wander());
    }

    void Update() { }

    void FixedUpdate() { }

    IEnumerator wander() {
        while (smoothIdx < smoothPoints.Length) {
            if (smoothIdx.Equals(smoothPoints.Length - 1)) {
                smoothPoints = getCurve(getBezierPoints(), spacing).ToArray();
                smoothIdx = 1;
            }

            Vector2 nextPoint = Vector2.MoveTowards(
                smoothPoints[smoothIdx - 1], 
                smoothPoints[smoothIdx], 
                Time.fixedDeltaTime * 0.25f
            );

            rb.MovePosition(nextPoint);
                
            yield return new WaitForSecondsRealtime(curve.Evaluate(smoothIdx / smoothPoints.Length) + 0.01f);

            smoothIdx += 1;

            // (curve.Evaluate(smoothIdx / smoothPoints.Length));
        }
    }

    private Vector2 getQuadraticBezier(Vector3 a, Vector3 b, Vector3 c, float t) {
        Vector2 p0 = Vector2.Lerp(a, b, t);
        Vector2 p1 = Vector2.Lerp(b, c, t);

        return Vector2.Lerp(p0, p1, t);
    }

    private Vector2 getCubicBezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t) {
        Vector2 p0 = getQuadraticBezier(a, b, c, t);
        Vector2 p1 = getQuadraticBezier(b, c, d, t);

        return Vector2.Lerp(p0, p1, t);
    }

    private Vector3[] getAnchorPoints() {
        Vector3[] anchorPoints = new Vector3[3];

        for (int i = 0; i < 3; i++) {
            anchorPoints[i] = new Vector3(Random.Range(-2, 2), Random.Range(-4, 4), 0);

            foreach (Vector3 item in anchorPoints) 
                if (anchorPoints[i].Equals(item))
                    anchorPoints[i] = new Vector3(Random.Range(-2, 2), Random.Range(-4, 4), 0);
        }

        return anchorPoints;
    }

    private Vector3[] getBezierPoints() {
        Vector3[] anchorPoints = getAnchorPoints();
        Vector3[] bezierPoints = new Vector3[numPoints];

        for (int i = 1; i < bezierPoints.Length + 1; i++) {
            float t = i / (float)bezierPoints.Length;
            bezierPoints[i - 1] = getCubicBezier(
                transform.position,
                anchorPoints[0], 
                anchorPoints[1], 
                anchorPoints[2], 
                t
            );
        }

        return bezierPoints;
    }

    public List<Vector3> getCurve(Vector3[] bezierPoints, float spacing) {
        List<Vector3> result = new List<Vector3>();

        spacing = spacing > 0.00001f ? spacing : 0.00001f;

        float offset = 0;
        while (offset < 0)
            offset += spacing;

        Vector2 current = bezierPoints[0];
        Vector2 next = bezierPoints[1];
        int last = bezierPoints.Length - 1;

        int i = 1;
        while (true) {
            Vector2 diff = next - current;
            float dist = diff.magnitude;

            if (dist >= offset) {
                current += diff * (offset / dist);
                result.Add(new Vector3(current.x, current.y, 0f));
                offset = spacing;
            }

            else if (i != last) {
                offset -= dist;
                current = next;
                next = bezierPoints[i++];
            }

            else break;
        }

        lineRenderer.positionCount = result.ToArray().Length;
        lineRenderer.SetPositions(result.ToArray());

        return result;
    }
}