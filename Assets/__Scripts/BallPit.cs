using UnityEngine;

public class BallPit : MonoBehaviour
{
    public AudioClip goalSound;
    private AudioSource audioSource;
    private bool triggered = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.volume = 0.2f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            if (goalSound != null)
            {
                audioSource.PlayOneShot(goalSound);
            }

            Invoke(nameof(FinishLevel), 0.5f);
        }
    }

    void FinishLevel()
    {
        GameManager.instance.PlayerReachedGoal();
    }
}