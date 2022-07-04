using UnityEngine;

public class PlaySpace : MonoBehaviour
{
    private Vector2 playSpace;

    void Start()
    {
        // move this to Update or LateUpdate for updated dimensions on res change
        playSpace = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)
        );
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, playSpace.x, playSpace.x * -1);
        viewPos.y = Mathf.Clamp(viewPos.y, playSpace.y, playSpace.y * -1);
        transform.position = viewPos;
    }
}
