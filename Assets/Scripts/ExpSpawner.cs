using UnityEngine;
using Mirror;

public class ExpSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject _expPrefab;

    public override void OnStartServer()
    {
        base.OnStartServer();

        SpawnExpObjects();
    }

    [Server]
    private void SpawnExpObjects()
    {
        Vector3[] directions =
        {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right,
            new Vector3(1, 1, 0),
            new Vector3(-1, 1, 0),
            new Vector3(1, -1, 0),
            new Vector3(-1, -1, 0)
        };

        foreach (var dir in directions)
        {
            Vector3 spawnPos = transform.position + dir * 2f;

            GameObject expObj = Instantiate(_expPrefab, spawnPos, Quaternion.identity);
            NetworkServer.Spawn(expObj);
        }
    }
}