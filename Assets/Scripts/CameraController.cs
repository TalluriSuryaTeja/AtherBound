using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target to Follow")]
    public Transform target;

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0f, 6f, -8f); // Height and distance behind player
    public float smoothSpeed = 5f;

    private void LateUpdate()
    {
        if (target == null) return;

        // Calculate where the camera should be
        Vector3 desiredPosition = target.position + offset;
        
        // Smoothly move the camera there
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Always look at the player's upper body (not their feet)
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
