using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardSlots;
    [SerializeField] private Image[] _slots;

    private bool[] _collected = new bool[6];

    public void ShowUI()
    {
        _cardSlots.SetActive(true);
    }

    public void OnCardCollected(Grade grade)
    {
        int index = (int)grade - 1;

        if (index < 0 || index >= _collected.Length)
        {
            return;
        }

        if (_collected[index])
        {
            Debug.Log("이미 먹은 카드");
            return;
        }

        _collected[index] = true;
        _slots[index].color = GetColor(grade);

        CheckActiveCount();
    }

    private Color GetColor(Grade grade)
    {
        switch (grade)
        {
            case Grade.Normal: return Color.white;
            case Grade.Rare: return Color.blue;
            case Grade.Epic: return new Color(0.6f, 0.2f, 1f);
            case Grade.Unique: return Color.yellow;
            case Grade.Legendary: return new Color(0.5f, 1f, 0f);
            case Grade.Mythic: return Color.red;
        }

        return Color.white;
    }

    private void CheckActiveCount()
    {
        int count = 0;

        for (int i = 0; i < _collected.Length; i++)
        {
            if (_collected[i])
            {
                count++;
            }
        }

        if (count == 2)
        {
            Debug.Log("2종 활성 효과 발동!");
        }
        else if (count == 4)
        {
            Debug.Log("4종 활성 효과 발동!");
        }
        else if (count == 6)
        {
            Debug.Log("풀 활성!");
        }
    }
}