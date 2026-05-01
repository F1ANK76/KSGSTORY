using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ExpSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject _expPrefab;

    private int _maxCount = 8;
    private List<GameObject> _current = new List<GameObject>();

    private Vector2 _center = Vector2.zero;
    private float _range = 5f;

    public override void OnStartServer()
    {
        for (int i = 0; i < _maxCount; i++)
        {
            Spawn();
        }
    }

    [Server]
    public void Spawn()
    {
        if (_current.Count >= _maxCount)
        {
            return;
        }

        Vector2 pos = GetRandomPos();

        GameObject obj = Instantiate(_expPrefab, pos, Quaternion.identity);
        NetworkServer.Spawn(obj);

        _current.Add(obj);

        ExpObject exp = obj.GetComponent<ExpObject>();
        exp.SetSpawner(this);
    }

    [Server]
    public void OnExpDestroyed(GameObject obj)
    {
        _current.Remove(obj);
        Spawn();
    }

    private Vector2 GetRandomPos()
    {
        return _center + new Vector2(
            Random.Range(-_range, _range),
            Random.Range(-_range, _range)
        );
    }
}