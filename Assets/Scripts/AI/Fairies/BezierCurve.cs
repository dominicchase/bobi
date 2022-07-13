using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour {
    void Start() { }

    void Update() { }

    void FixedUpdate() { }

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

    public Vector3[] getBezierPoints() {
        Vector3[] anchorPoints = getAnchorPoints();
        Vector3[] bezierPoints = new Vector3[50];

        bezierPoints[0] = transform.position;

        for (int i = 1; i < bezierPoints.Length; i++) {
            float t = i / (float)bezierPoints.Length;
            bezierPoints[i] = getCubicBezier(
                transform.position,
                anchorPoints[0], 
                anchorPoints[1], 
                anchorPoints[2], 
                t
            );
        }

        return bezierPoints;
    }

    public List<Vector3> getBezierCurve(Vector3[] bezierPoints) {
        List<Vector3> result = new List<Vector3>();

        float spacing = 0.05f > 0.00001f ? 0.05f : 0.00001f;

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

        return result;
    }
}