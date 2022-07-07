using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FriendlyController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 nextPointFollow = new Vector2(0f, 0f);
    Vector2 nextPointWander = new Vector2(0f, 0f);
    public float speed = 0.1f;

    public GameObject player;

    private bool isWandering = false;
    public LineRenderer lineRenderer;
    private int numPoints = 500;
    private Vector3[] points = new Vector3[500];

    // TODO: 
    // - if player holds down, reset bevier curve from friend's new position
    // - make sure friend can collider with objs (dynamic/kinematic)
    // - make sure if friend collides with obj, recalc bezier curve

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        setBezierCurve();

        StartCoroutine(wander());
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
            getNextPointFollow();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
            follow();
    }

    #region friend behaviors

    void follow()
    {
        rb.MovePosition(nextPointFollow);
    }

    #endregion

    #region friend helper functions

    void getNextPointFollow()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        nextPointFollow = Vector2.Lerp(transform.position, player.transform.position, speed);
    }

    IEnumerator wander()
    {
        for (int i = 1; i < points.Length; i++)
        {
            nextPointWander = Vector2.Lerp(points[i - 1], points[i], speed * Time.deltaTime);
            rb.MovePosition(nextPointWander);
            yield return new WaitForSeconds(0.02f);
        }
    }

    #endregion

    #region bezier curve functions

    private Vector3 getBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        return (((1 - t) * (1 - t)) * p0) + (2 * (1 - t) * t * p1) + (t * t * p2);
    }

    private void setBezierCurve()
    {

        GameObject p1 = new GameObject();
        p1.transform.position = new Vector3(Random.Range(-2, 2), Random.Range(-4, 4), 0);

        GameObject p2 = new GameObject();
        p2.transform.position = new Vector3(Random.Range(-2, 2), Random.Range(-4, 4), 0);

        lineRenderer.positionCount = numPoints;

        for (int i = 1; i < numPoints + 1; i++)
        {
            float t = i / (float)numPoints;
            points[i - 1] = getBezierPoint(transform.position, p1.transform.position, p2.transform.position, t);
        }

        lineRenderer.SetPositions(points);

        Destroy(p1);
        Destroy(p2);
    }

    #endregion
}
