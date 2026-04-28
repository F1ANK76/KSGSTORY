using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private WeaponHit _weaponHit;
    private bool _isAttacking;

    public Transform weaponPivot;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isAttacking)
        {
            StartCoroutine(Swing());
        }
    }

    IEnumerator Swing()
    {
        _isAttacking = true;
        _weaponHit.canHit = true; // 공격 시작

        float duration = 0.5f;
        float time = 0f;

        float startAngle = 0f;
        float endAngle = -90f;

        // 앞으로
        while (time < duration * 0.5f)
        {
            time += Time.deltaTime;
            float t = time / (duration * 0.5f);

            float angle = Mathf.Lerp(startAngle, endAngle, t);
            weaponPivot.localRotation = Quaternion.Euler(0, 0, angle);

            yield return null;
        }

        time = 0f;

        // 돌아오기
        while (time < duration * 0.5f)
        {
            time += Time.deltaTime;
            float t = time / (duration * 0.5f);

            float angle = Mathf.Lerp(endAngle, startAngle, t);
            weaponPivot.localRotation = Quaternion.Euler(0, 0, angle);

            yield return null;
        }

        weaponPivot.localRotation = Quaternion.Euler(0, 0, startAngle);

        _isAttacking = false;
        _weaponHit.canHit = false; // 공격 끝
    }
}