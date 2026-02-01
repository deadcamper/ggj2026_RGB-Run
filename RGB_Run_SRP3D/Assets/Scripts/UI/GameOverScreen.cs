using System;
using UnityEngine;
using UnityEngine.UI;
using R3;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Text _scoreLabel;

    private void OnEnable()
    {
        var score = Services.instance.Get<GameStateManager>().Score;
        _scoreLabel.text = $"Score: {score}";
        
        _restartButton.OnClickAsObservable()
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Subscribe(_ =>
            {
                Services.instance.Get<GameStateManager>()?.SetGameState(GameStateManager.GameStateType.Playing);
            })
            .AddTo(this);
        
        _mainMenuButton.OnClickAsObservable()
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Subscribe(_ => SceneManager.LoadScene("MainMenu"))
            .AddTo(this);
    }
}