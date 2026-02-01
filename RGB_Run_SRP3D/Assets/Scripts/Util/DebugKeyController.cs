using System;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugKeyController : MonoBehaviour
{
    private void OnEnable()
    {
        var addScoreAction = InputSystem.actions.FindAction("DebugAddScore");
        ObservableUtils.InputActionStartedAsObservable(addScoreAction)
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Subscribe(_ =>
            {
                Services.instance.Get<ScoreManager>()?.AddScore(1u);
                // Debug.Log($"Add score pressed");
            })
            .AddTo(this);
        
        //debug log
        Services.instance.Get<ScoreManager>().Score
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Subscribe(score =>
            {
                Debug.Log($"Score updated: {score}");
            })
            .AddTo(this);
    }
}