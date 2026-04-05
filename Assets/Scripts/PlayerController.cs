using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (other.CompareTag("Exp"))
        {
            CmdCollectExp(other.gameObject);
        }
    }

    [Command]
    private void CmdCollectExp(GameObject expObj)
    {
        ExpObject exp = expObj.GetComponent<ExpObject>();

        if (exp != null)
        {
            exp.Collect();
        }
    }
}