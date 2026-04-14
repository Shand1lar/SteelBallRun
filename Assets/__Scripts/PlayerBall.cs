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

    public AudioClip fallSound;
    private AudioSource audioSource;
    [Header("Audio")]
        public AudioClip moveSound;

        private AudioSource moveAudioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnPosition = transform.position;
        
        audioSource = GetComponent<AudioSource>();

            if (audioSource == null)
                {
                audioSource = gameObject.AddComponent<AudioSource>();
                }

        audioSource.volume = 0.2f;

        moveAudioSource = gameObject.AddComponent<AudioSource>();
        moveAudioSource.clip = moveSound;
        moveAudioSource.loop = true;
        moveAudioSource.playOnAwake = false;
        moveAudioSource.volume = 0.01f;
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

        bool isMoving = Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f;

        if (isMoving)
        {
            if (!moveAudioSource.isPlaying && moveSound != null)
            {
                moveAudioSource.Play();
            }
        }
        else
        {
            if (moveAudioSource.isPlaying)
            {
                moveAudioSource.Stop();
            }
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
        if (fallSound != null)
            {
            audioSource.PlayOneShot(fallSound);
            }
    }

    public void FreezeVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}