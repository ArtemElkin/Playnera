using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private bool dragging = false;
    private CameraController cameraController;
    private Vector2 mousePos, offset;
    private Camera cam;
    private Rigidbody2D targetItemRB;
    private Transform targetItemTransform;
    void Awake()
    {
        cam = Camera.main;
        cameraController = GetComponent<CameraController>();
    }
    private void BeginDragging()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("item"))
        {
            targetItemRB = hit.collider.GetComponent<Rigidbody2D>();
            targetItemTransform = hit.transform;

            offset =  mousePos - new Vector2(targetItemTransform.position.x, targetItemTransform.position.y);
            dragging = true;
            cameraController.blockMovement = true;
            targetItemRB.gravityScale = 0f;
        }
    }
    private void Drag()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        targetItemTransform.position = new Vector3(mousePos.x - offset.x, mousePos.y - offset.y, targetItemTransform.position.z);
    }
    private void Drop()
    {
        dragging = false;
        cameraController.blockMovement = false;
        targetItemRB.gravityScale = 1f;
    }
    void Update()
    {
        if (dragging)
        {
            if (Input.GetMouseButton(0))
                Drag();
            else if (Input.GetMouseButtonUp(0))
                Drop();
        } else if (Input.GetMouseButtonDown(0))
            BeginDragging();
    }
}
