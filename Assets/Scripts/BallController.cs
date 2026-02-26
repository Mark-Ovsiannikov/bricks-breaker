using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    public float launchSpeed = 7f;
    public float maxSpeed = 9f;

    Rigidbody2D _rb;
    bool _launched;
    Transform _paddle;
    Vector2 _offsetFromPaddle;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void AttachToPaddle(Transform paddle)
    {
        _paddle = paddle;
        _offsetFromPaddle = new Vector2(0f, 0.6f);
        _launched = false;
        _rb.linearVelocity = Vector2.zero;
        transform.position = _paddle.position + (Vector3)_offsetFromPaddle;
    }

    void Update()
    {
        if (!_launched && _paddle != null)
        {
            transform.position = _paddle.position + (Vector3)_offsetFromPaddle;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Launch();
            }
        }
    }

    void FixedUpdate()
    {
        if (_launched)
        {
            var v = _rb.linearVelocity;
            var sp = v.magnitude;

            if (sp < 0.01f)
            {
                _rb.linearVelocity = Vector2.up * launchSpeed;
                return;
            }

            if (sp > maxSpeed)
                _rb.linearVelocity = v.normalized * maxSpeed;
        }
    }

    void Launch()
    {
        _launched = true;

        float x = Random.Range(-0.8f, 0.8f);
        Vector2 dir = new Vector2(x, 1f).normalized;

        _rb.linearVelocity = dir * launchSpeed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!_launched) return;

        var v = _rb.linearVelocity;
        v += Random.insideUnitCircle * 0.05f;
        _rb.linearVelocity = v.normalized * Mathf.Clamp(v.magnitude, 0f, maxSpeed);
    }
}