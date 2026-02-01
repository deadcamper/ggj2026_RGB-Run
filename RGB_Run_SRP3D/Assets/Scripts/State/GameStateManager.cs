using System;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public enum GameStateType
    {
        Playing,
        GameOver
    }
    
    public ReadOnlyReactiveProperty<GameStateType> GameState => _gameState;
    private ReactiveProperty<GameStateType> _gameState = new();
    
    public ReadOnlyReactiveProperty<uint> Score => _score;
    private readonly ReactiveProperty<uint> _score = new();

    void Awake()
    {
        Services.instance.Set(this);
    }

    private void OnDestroy()
    {
        Services.instance.Set<GameStateManager>(null);
    }

    public void SetGameState(GameStateType gameState)
    {
        if (gameState == GameStateType.Playing && _gameState.Value != GameStateType.Playing)
        {
            // Reload scene to reset game state
            SceneManager.LoadScene("RunnerGame");
        } 
        _gameState.Value = gameState;
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
