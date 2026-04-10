using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;

    // Isometric offset matching the screenshot angle
    private Vector3 offset = new Vector3(0, 22, -18);

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 desired = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desired,
                                          smoothSpeed * Time.deltaTime);
        // Fixed isometric rotation — do not use LookAt
        transform.rotation = Quaternion.Euler(52, 0, 0);
    }
}