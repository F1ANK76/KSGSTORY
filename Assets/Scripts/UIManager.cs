using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _cardSlots;
    [SerializeField] private Image[] _slots;
    [SerializeField] private Image[] _skillImgs;

    private bool[] _collected = new bool[6];
    private bool[] _skillUnlocked = new bool[3];
    private Color _activeColor = Color.green;

    private Player _player;

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    public bool IsSkillUnlocked(int index)
    {
        return _skillUnlocked[index];
    }

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
            UnlockSkill(0);
        }
        else if (count == 4)
        {
            UnlockSkill(1);
        }
        else if (count == 6)
        {
            UnlockSkill(2);
        }
    }

    private void UnlockSkill(int index)
    {
        if (_skillUnlocked[index])
        {
            return;
        }

        _skillUnlocked[index] = true;
        _skillImgs[index].color = _activeColor;

        if (index == 0)
        {
            _player.CmdSetSwordScaleY(10f);
        }
        else if (index == 1)
        {
            _player.CmdSetSwordScaleX(0.7f);
        }
        else if (index == 2)
        {
            if (_player != null)
            {
                PlayerAttack attack = _player.GetComponent<PlayerAttack>();

                if (attack != null)
                {
                    attack.SetAttackAngle(360f);
                }
            }
        }
    }
}