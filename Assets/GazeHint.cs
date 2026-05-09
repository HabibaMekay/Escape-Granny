using UnityEngine;

public class GazeHint : MonoBehaviour
{
    public GameObject hintCanvas; 
    public Transform playerCamera;
    
    [Header("Settings")]
    public float maxDetectionDistance = 3.0f; // Show when within 3 meters
    public float minDetectionDistance = 0.1f; // Don't turn off when close
    public float lookAngle = 40f;             // Wider angle for comfort

    void Update()
    {
        if (playerCamera == null || hintCanvas == null) return;

        // Calculate vector from camera to door
        Vector3 dirToDoor = (transform.position - playerCamera.position).normalized;
        
        // Calculate angle and distance
        float angle = Vector3.Angle(playerCamera.forward, dirToDoor);
        float distance = Vector3.Distance(transform.position, playerCamera.position);

        // FIX: The hint stays on if you are between 0.1m and 3.0m AND looking at it
        bool isLooking = (angle < lookAngle);
        bool isNear = (distance < maxDetectionDistance && distance > minDetectionDistance);

        hintCanvas.SetActive(isLooking && isNear);
    }
}