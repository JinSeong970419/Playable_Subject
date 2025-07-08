using System;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [Header("스코어 세팅")]
    public int baseScore = 10;
    public int comboBonus = 5;
    public float comboResetTime = 2f;

    private int _score = 0;
    private int _combo = 0;

    public Action<int> OnScoreChanged;

    public void AddScore()
    {
        int added = baseScore + (_combo * comboBonus);
        _score += added;

        _combo++;

        if (OnScoreChanged != null)
            OnScoreChanged(_score);
    }

    public void ResetCombo()
    {
        _combo = 0;
    }

    public void ResetScore()
    {
        _score = 0;
        _combo = 0;

        if (OnScoreChanged != null)
            OnScoreChanged(_score);
    }

    public int GetScore() => _score;
    public int GetCombo() => _combo;
}
