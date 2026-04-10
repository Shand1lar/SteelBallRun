using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    [Header("Movement")]
    public float moveForce    = 15f;
    public float maxSpeed     = 8f;

    [Header("World Settings")]
    public float surfaceY      = 1.7f;
    public float fallThreshold = -2f;

    [HideInInspector] public bool inputEnabled = true;

    private Rigidbody rb;
    private Vector3 spawnPosition;

    void Start()
    {
        rb            = GetComponent<Rigidbody>();
        spawnPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (inputEnabled) Move();
        ClampToSurface();
        CheckFallOff();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        rb.AddForce(new Vector3(h, 0, v) * moveForce);

        Vector3 flat = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (flat.magnitude > maxSpeed)
        {
            Vector3 clamped = flat.normalized * maxSpeed;
            rb.velocity = new Vector3(clamped.x, rb.velocity.y, clamped.z);
        }
    }

    void ClampToSurface()
    {
        if (transform.position.y < surfaceY)
        {
            transform.position = new Vector3(
                transform.position.x, surfaceY, transform.position.z);
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
    }

    void CheckFallOff()
    {
        if (transform.position.y < fallThreshold)
        {
            rb.velocity        = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = spawnPosition;
        }
    }

    public void FreezeVelocity()
    {
        rb.velocity        = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}