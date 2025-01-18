using UnityEngine;
public class Scaler : MonoBehaviour
{
    public float scalingTime = 0.1f;
    private bool isScaling = false;
    private Vector3 targetScale;
    private Vector3 velocity = Vector3.zero;
    void Update()
    {
        if (isScaling)
            Scale();
    }
    private void Scale()
    {
        transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref velocity, scalingTime);
        if (Vector3.Distance(transform.localScale, targetScale) <= 0.1f)
        {
            transform.localScale = targetScale;
            isScaling = false;
        }
    }
    private void SetTargetScale(DepthDegree depth)
    {
        switch(depth)
        {
            case DepthDegree.NEAR:
                targetScale = new Vector3(0.6f, 0.5f, 1);
                break;
            case DepthDegree.MID:
                targetScale = new Vector3(0.4f, 0.35f, 1);
                break;
            case DepthDegree.FAR:
                targetScale = new Vector3(0.2f, 0.25f, 1);
                break;
        }
        isScaling = true;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isScaling)
            return;
        if (collider.CompareTag("slot"))
            SetTargetScale(collider.GetComponent<Slot>().depth);
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (isScaling)
           return;
        if (collider.CompareTag("slot"))
            SetTargetScale(DepthDegree.MID);
    }
}
