using UnityEngine;

public class BottomZone : MonoBehaviour
{
    public GameManager gm;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ball")) return;
        gm.OnBallLost();
    }
}