using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool blockMovement = false;
    [SerializeField]
    private SpriteRenderer background;
    private float backgroundWidth, cameraWidth, leftBorderX, rightBorderX, mousePosX, newPosX, offset, startPosX;
    private Camera cam;

    void Awake()
    {
        cam = Camera.main;
        backgroundWidth = background.bounds.size.x;
        cameraWidth = cam.orthographicSize * 2f * Screen.width / Screen.height;
        // Boundaries of camera movement
        leftBorderX = - backgroundWidth / 2f + cameraWidth / 2f;
        rightBorderX = backgroundWidth / 2f - cameraWidth / 2f;

    }
    private void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosX = cam.ScreenToWorldPoint(Input.mousePosition).x;
        } else if (Input.GetMouseButton(0))
        {
            mousePosX = cam.ScreenToWorldPoint(Input.mousePosition).x;
            offset =  mousePosX - startPosX;
            newPosX = transform.position.x - offset; 
            newPosX = Mathf.Clamp(newPosX, leftBorderX, rightBorderX);
            transform.position = new Vector3(newPosX ,transform.position.y, transform.position.z);
        }
    }
    void Update()
    {
        if (!blockMovement)
            Move();
    }
}
