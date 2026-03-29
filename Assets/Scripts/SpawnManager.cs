using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject monsterPrefab;
    public Transform spawnPoint;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        Instantiate(monsterPrefab, spawnPoint.position, Quaternion.identity);
    }
}