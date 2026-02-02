using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using R3;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _newGameButton;

    // Debug area
    public bool forceHideDebugArea = false;
    [SerializeField] private GameObject _debugLevelSelectArea;

    // Debug scenes
    [SerializeField] private Button _eliotTestScene1SceneButton;
    [SerializeField] private Button _testObstaclesSceneButton;
    [SerializeField] private Button _testRailRunnerSceneButton;
    [SerializeField] private Button _testSoundBoardSceneButton;
    [SerializeField] private Button _testTrackSpawnerSceneButton;
    [SerializeField] private Button _testInputSystemSceneButton;
    
    void OnEnable()
    {
        _newGameButton.OnClickAsObservable()
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Subscribe(_ => SceneManager.LoadScene("RunnerGame"))
            .AddTo(this);
        
        // Debug scenes
        _eliotTestScene1SceneButton.OnClickAsObservable()
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Subscribe(_ => SceneManager.LoadScene("EliotTestScene1"))
            .AddTo(this);
        
        _testObstaclesSceneButton.OnClickAsObservable()
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Subscribe(_ => SceneManager.LoadScene("Test_ObstaclesScene"))
            .AddTo(this);
        
        _testRailRunnerSceneButton.OnClickAsObservable()
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Subscribe(_ => SceneManager.LoadScene("Test_RailRunner"))
            .AddTo(this);
        
        _testSoundBoardSceneButton.OnClickAsObservable()
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Subscribe(_ => SceneManager.LoadScene("Test_SoundBoard"))
            .AddTo(this);

        _testTrackSpawnerSceneButton.OnClickAsObservable()
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Subscribe(_ => SceneManager.LoadScene("Test_TrackSpawner"))
            .AddTo(this);
        
        _testInputSystemSceneButton.OnClickAsObservable()
            .TakeUntil(_ => !this.isActiveAndEnabled)
            .Subscribe(_ => SceneManager.LoadScene("TestInputSystem"))
            .AddTo(this);

        // Hide the debug area if we're not in editor mode
        _debugLevelSelectArea.SetActive(!forceHideDebugArea & Application.isEditor);
    }
}
