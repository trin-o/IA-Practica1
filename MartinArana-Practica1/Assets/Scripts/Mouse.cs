using UnityEngine;

public class Mouse : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        transform.position = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
