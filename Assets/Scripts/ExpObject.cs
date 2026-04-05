using UnityEngine;
using Mirror;

public class ExpObject : NetworkBehaviour
{
    [Server]
    public void Collect()
    {
        NetworkServer.Destroy(gameObject);
    }
}