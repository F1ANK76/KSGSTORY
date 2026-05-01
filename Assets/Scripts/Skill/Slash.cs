using UnityEngine;

public class Slash : MonoBehaviour
{
    private float _duration = 0.3f;
    private float _elapsed = 0f;

    private float _startAngle;
    private float _endAngle;

    public void Init(bool isRight)
    {
        if (isRight)
        {
            _startAngle = -90f;
            _endAngle = 90f;
        }
        else
        {
            _startAngle = 90f;
            _endAngle = -90f;
        }
    }

    private void Update()
    {
        _elapsed += Time.deltaTime;

        float t = _elapsed / _duration;
        float angle = Mathf.Lerp(_startAngle, _endAngle, t);

        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (_elapsed >= _duration)
        {
            Destroy(gameObject);
        }
    }
}