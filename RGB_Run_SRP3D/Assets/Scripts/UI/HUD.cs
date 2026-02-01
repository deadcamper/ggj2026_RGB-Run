using UnityEngine;
using UnityEngine.UI;
using R3;

public class HUD : MonoBehaviour
{
    [SerializeField] private Text _scoreLabel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        ScoreManager scoreMgr = Services.instance.Get<ScoreManager>();
        if (! scoreMgr)
        {
            Debug.LogError("Failed to find the ScoreManager in Services. HUD will have default texts.", this);
            return;
        }
        Services.instance.Get<ScoreManager>().Score
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .SubscribeToText(_scoreLabel, score=> $"Score: {score}")
            .AddTo(this);
    }
}
