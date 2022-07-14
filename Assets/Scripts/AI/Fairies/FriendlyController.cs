using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyController : MonoBehaviour
{
    GameObject player;
    Vector3 pathToPlayer;

    Vector3 direction;
    Ray ray;
    private bool collision = false;

    [SerializeField] int speed;

    #region UnityFunctions

    void Start() { player = PlayerManager.instance.player; }

    void Update() { 
        getPathToPlayer(); 
        handleCollision();
    }

    void FixedUpdate() { 
        if (Input.GetMouseButton(0) && !collision) { follow(); }
    }

    #endregion

    #region FairyActions

    void follow() { transform.position = pathToPlayer; }

    #endregion

    #region CollisionFunctions

    private void handleCollision() {
        direction = (player.transform.position - transform.position).normalized;
        ray = new Ray(transform.position, direction);
        Debug.DrawRay(transform.position, direction, Color.red);

        Vector3 rightD = direction - new Vector3(Mathf.Cos(45f), Mathf.Sin(45f), 0).normalized;
        Ray rightR = new Ray(transform.position, rightD);
        Debug.DrawRay(transform.position, rightD, Color.blue);

        if (Physics.Raycast(ray, out RaycastHit hit, direction.x)) { 
            collision = true;
            Debug.Log("Hit!"); 
        } else {
            collision = false;
        }
    }

    #endregion

    #region HelperFunctions

    void getPathToPlayer() { 
        pathToPlayer = Vector3.Lerp(transform.position, player.transform.position, speed * Time.deltaTime
        );
    }

    #endregion
}