using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    [Header("Movement")]
    public float moveForce = 15f;
    public float maxSpeed = 8f;

    [Header("Respawn")]
    public float fallThreshold = -2f;
    public float respawnHeightOffset = 0.5f;

    [HideInInspector] public bool inputEnabled = true;

    private Rigidbody rb;
    private Vector3 spawnPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (inputEnabled)
            Move();

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

    void CheckFallOff()
    {
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = spawnPosition + Vector3.up * respawnHeightOffset;
    }

    public void FreezeVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}