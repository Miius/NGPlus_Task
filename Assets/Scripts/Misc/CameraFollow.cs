using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;

    [Header("Follow Settings")]
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Vector3 offset = new Vector3(0, 10, -10);

    [Header("Camera Limits (World Space)")]
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float minZ = -10f;
    [SerializeField] private float maxZ = 10f;

    private void FixedUpdate() 
    {
        if (target == null) return;
        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        smoothPos.x = Mathf.Clamp(smoothPos.x, minX, maxX);
        smoothPos.z = Mathf.Clamp(smoothPos.z, minZ, maxZ);

        transform.position = smoothPos;
    }
}