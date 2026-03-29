using UnityEngine;

public class MonsterMover : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _moveDistance = 2.0f;
    private float _moveSpeed = 2f;
    private float _startX;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startX = transform.position.x;
    }

    private void FixedUpdate()
    {
        float targetX = _startX + Mathf.Sin(Time.time * _moveSpeed) * _moveDistance;

        float velocityX = (targetX - transform.position.x) * 10f;

        _rb.linearVelocity = new Vector2(velocityX, _rb.linearVelocity.y);
    }
}