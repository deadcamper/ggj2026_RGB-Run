using System;
using R3;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public ReadOnlyReactiveProperty<uint> Score => _score;
    private readonly ReactiveProperty<uint> _score = new();

    void Awake()
    {
        Services.instance.Set(this);
    }

    private void OnDestroy()
    {
        Services.instance.Set<ScoreManager>(null);
    }

    public void AddScore(uint score)
    {
        // Debug.Log($"AddScore({score})");
        // Debug.Log($"Old score:{_score.Value}");
        _score.Value += score;
        // Debug.Log($"New score:{_score.Value}");
    }

    public void ResetScore()
    {
        _score.Value = 0;
    }
}
