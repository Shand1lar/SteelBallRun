using UnityEngine;

public class BallPit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.PlayerReachedGoal();
        }
    }
}