using UnityEngine;

public class WeaponHit : MonoBehaviour
{
    public bool canHit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canHit)
        {
            return;
        }

        if (other.CompareTag("Exp"))
        {
            ExpObject exp = other.GetComponent<ExpObject>();

            if (exp != null)
            {
                exp.Collect();
            }
        }
    }
}