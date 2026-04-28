using UnityEngine;
using Mirror;

public class ExpObject : NetworkBehaviour
{
    [Header("Grade Prefabs (Normal ~ Mythic ¼ø¼­)")]
    [SerializeField] private GameObject[] _gradePrefabs;

    [Server]
    public void Collect()
    {
        Grade grade = GetRandomGrade();

        Debug.Log($"È¹µæ µî±Þ: {grade}");

        SpawnGradeObject(grade);

        NetworkServer.Destroy(gameObject);
    }

    private Grade GetRandomGrade()
    {
        float rand = Random.value;

        if (rand < 0.001f) return Grade.Mythic;      // 0.1%
        else if (rand < 0.005f) return Grade.Legendary; // 0.4%
        else if (rand < 0.03f) return Grade.Unique;     // 2.5%
        else if (rand < 0.08f) return Grade.Epic;       // 5%
        else if (rand < 0.20f) return Grade.Rare;       // 12%
        else if (rand < 0.50f) return Grade.Normal;     // 30%
        else return Grade.None;                         // 50%
    }

    [Server]
    private void SpawnGradeObject(Grade grade)
    {
        if (grade == Grade.None)
        {
            return;
        }

        int index = (int)grade - 1;

        if (index < 0 || index >= _gradePrefabs.Length)
        {
            return;
        }

        GameObject prefab = _gradePrefabs[index];

        GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
        NetworkServer.Spawn(obj);
    }
}