using UnityEngine;
using Mirror;

public class Card : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SyncVar(hook = nameof(OnGradeChanged))]
    private Grade _grade;

    public void Init(Grade grade)
    {
        _grade = grade;
        ApplyColor(grade);
    }

    private void OnGradeChanged(Grade oldGrade, Grade newGrade)
    {
        ApplyColor(newGrade);
    }

    private void ApplyColor(Grade grade)
    {
        _spriteRenderer.color = GetColor(grade);
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

    public Grade GetGrade()
    {
        return _grade;
    }
}