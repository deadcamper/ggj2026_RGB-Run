using System;
using UnityEngine;
using R3;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private HUD _hud;
    [SerializeField] private GameOverScreen _gameOverScreen;

    private void Start()
    {
        // Listen for change back to playing state
        Services.instance.Get<GameStateManager>().GameState
            .AsObservable()
            .DistinctUntilChanged()
            .TakeWhile(_ => this.isActiveAndEnabled)
            .Subscribe(gameState =>
            {
                Debug.Log($"GameUIManager observed gamestate change to: {gameState}");
                switch (gameState)
                {
                    case GameStateManager.GameStateType.Playing:
                    {
                        _hud.gameObject.SetActive(true);
                        _gameOverScreen.gameObject.SetActive(false);
                        return;
                    }
                    case GameStateManager.GameStateType.GameOver:
                    {
                        _hud.gameObject.SetActive(false);
                        _gameOverScreen.gameObject.SetActive(true);
                        return;
                    }
                    default:
                        throw new ArgumentException($"Unexpected game state: {gameState}", nameof(gameState));
                }
            })
            .AddTo(this);
    }
}