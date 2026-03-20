using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public Transform WeaponPivot;

    private bool _isAttacking;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !_isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        _isAttacking = true;

        float time = 0f;

        Quaternion startRot = WeaponPivot.localRotation;
        Quaternion downRot = Quaternion.Euler(0, 0, -90f);

        while (time < 0.25f)
        {
            time += Time.deltaTime;
            WeaponPivot.localRotation = Quaternion.Lerp(startRot, downRot, time / 0.25f);
            yield return null;
        }

        time = 0f;

        while (time < 0.25f)
        {
            time += Time.deltaTime;
            WeaponPivot.localRotation = Quaternion.Lerp(downRot, startRot, time / 0.25f);
            yield return null;
        }

        _isAttacking = false;
    }
}