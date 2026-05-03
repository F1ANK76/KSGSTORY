using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private WeaponHit _weaponHit;
    [SerializeField] private float _attackAngle = 90f;

    private bool _isAttacking;

    public Transform weaponPivot;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isAttacking)
        {
            StartCoroutine(Swing());
        }
    }

    public void SetAttackAngle(float angle)
    {
        _attackAngle = angle;
    }

    IEnumerator Swing()
    {
        _isAttacking = true;
        _weaponHit.canHit = true;

        float duration = 0.5f;
        float half = duration * 0.5f;

        float rotated = 0f;
        float target = _attackAngle;

        while (rotated < target)
        {
            float delta = target / half * Time.deltaTime;

            if (rotated + delta > target)
            {
                delta = target - rotated;
            }

            weaponPivot.Rotate(0f, 0f, -delta);
            rotated += delta;

            yield return null;
        }

        rotated = 0f;

        while (rotated < target)
        {
            float delta = target / half * Time.deltaTime;

            if (rotated + delta > target)
            {
                delta = target - rotated;
            }

            weaponPivot.Rotate(0f, 0f, delta);
            rotated += delta;

            yield return null;
        }

        _isAttacking = false;
        _weaponHit.canHit = false;
    }
}