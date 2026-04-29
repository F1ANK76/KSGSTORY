using UnityEngine;
using Mirror;

public class ExpObject : NetworkBehaviour
{
    [Header("Card Prefab (1°³¸¸ »ç¿ë)")]
    [SerializeField] private GameObject _cardPrefab;

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

        if (rand < 0.001f) return Grade.Mythic;
        else if (rand < 0.005f) return Grade.Legendary;
        else if (rand < 0.03f) return Grade.Unique;
        else if (rand < 0.08f) return Grade.Epic;
        else if (rand < 0.20f) return Grade.Rare;
        else if (rand < 0.50f) return Grade.Normal;
        else return Grade.None;
    }

    [Server]
    private void SpawnGradeObject(Grade grade)
    {
        if (grade == Grade.None)
        {
            return;
        }

        GameObject obj = Instantiate(_cardPrefab, transform.position, Quaternion.identity);

        Card card = obj.GetComponent<Card>();

        if (card != null)
        {
            card.Init(grade);
        }

        NetworkServer.Spawn(obj);
    }
}