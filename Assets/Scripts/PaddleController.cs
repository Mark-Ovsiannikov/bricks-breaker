using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 12f;

    [Header("Clamp")]
    public float minX = -10f;
    public float maxX = 10f;

    float _input;

    void Update()
    {
        _input = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        var pos = transform.position;
        pos.x += _input * speed * Time.fixedDeltaTime;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;
    }
}