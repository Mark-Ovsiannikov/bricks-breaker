using UnityEngine;

public class BrickHit : MonoBehaviour
{
    public GameManager gm;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.CompareTag("Ball")) return;

        var brick = GetComponent<Brick>();
        int pts = brick != null ? brick.points : 10;

        gm.OnBrickDestroyed(pts);
        Destroy(gameObject);
    }
}