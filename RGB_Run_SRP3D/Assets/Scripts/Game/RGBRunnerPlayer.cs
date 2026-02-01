using UnityEngine;
using UnityEngine.Events;
using R3;

public class RGBRunnerPlayer : MonoBehaviour
{
    public RailRunner railRunner;

    public GameObject playerModel;

    public bool debugModeOn = false;

    public UnityEvent OnBadSegmentTrigger;

    public UnityEvent OnGoodSegmentTrigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Good
        OnGoodSegmentTrigger.AddListener(() => {
            Services.instance.Get<GameStateManager>()?.AddScore(1);
            Services.instance.Get<AudioManager>()?.PlaySound(AudioManager.SoundEventType.Whoosh);
        });

        // Bad
        OnBadSegmentTrigger.AddListener(() =>
        {
            Services.instance.Get<GameStateManager>()?.SetGameState(GameStateManager.GameStateType.GameOver);
            playerModel.SetActive(false);
            Services.instance.Get<AudioManager>()?.PlaySound(AudioManager.SoundEventType.ObstacleHit);
        });
        
        // Listen for change back to playing state
        Services.instance.Get<GameStateManager>().GameState
            .AsObservable()
            .DistinctUntilChanged()
            .Where(gameState => gameState == GameStateManager.GameStateType.Playing)
            .TakeWhile(_ => this.isActiveAndEnabled)
            .Subscribe(gameState =>
            {
                Debug.Log($"RGBRunnerPlayer observed gamestate change to: {gameState}");
                playerModel.SetActive(true);
            })
            .AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Debug buttons
    void OnActionOne()
    {
        if(debugModeOn)
            railRunner.JumpToRailByIndex(0);
    }

    void OnActionTwo()
    {
        if (debugModeOn)
            railRunner.JumpToRailByIndex(1);
    }

    void OnActionThree()
    {
        if (debugModeOn)
            railRunner.JumpToRailByIndex(2);
    }
    #endregion

    void OnMoveLeft()
    {
        railRunner.JumpToRailByOffset(-1);
    }

    void OnMoveRight()
    {
        railRunner.JumpToRailByOffset(1);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BadSegment")
        {
            Debug.Log("Triggered Bad Segment!");
            OnBadSegmentTrigger.Invoke();
        }
        else if(other.tag == "GoodSegment")
        {
            Debug.Log("Triggered Good Segment!");
            OnGoodSegmentTrigger.Invoke();
        }
    }

}
