using Mirror;
using UnityEngine;

public class Slash : MonoBehaviour
{
    private float _duration = 0.5f;
    private float _elapsed = 0f;

    private float _rotateSpeed = 360f; // 0.5초에 180도

    private void Update()
    {
        _elapsed += Time.deltaTime;

        // 왼쪽 방향으로 회전
        transform.Rotate(0f, 0f, _rotateSpeed * Time.deltaTime);

        if (_elapsed >= _duration)
        {
            Destroy(gameObject);
        }
    }

    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D other)
    {
        ExpObject exp = other.GetComponentInParent<ExpObject>();

        if (exp != null)
        {
            exp.Collect();
        }
    }
}