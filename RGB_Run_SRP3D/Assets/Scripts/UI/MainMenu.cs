using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using R3;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _newGameButton;
    
    // Debug scenes
    [SerializeField] private Button _testRailRunnerSceneButton;
    [SerializeField] private Button _testInputSystemSceneButton;
    [SerializeField] private Button _eliotTestScene1SceneButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        _newGameButton.OnClickAsObservable()
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Subscribe(_ => SceneManager.LoadScene("RunnerGame"))
            .AddTo(this);
        
        // Debug scenes
        _testRailRunnerSceneButton.OnClickAsObservable()
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Subscribe(_ => SceneManager.LoadScene("Test_RailRunner"))
            .AddTo(this);
        
        _testInputSystemSceneButton.OnClickAsObservable()
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Subscribe(_ => SceneManager.LoadScene("TestInputSystem"))
            .AddTo(this);
        
        _eliotTestScene1SceneButton.OnClickAsObservable()
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Subscribe(_ => SceneManager.LoadScene("EliotTestScene1"))
            .AddTo(this);
    }
}
