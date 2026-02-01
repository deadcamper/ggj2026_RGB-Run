using UnityEngine;
using UnityEngine.Events;

public class RGBRunnerPlayer : MonoBehaviour
{
    public RailRunner railRunner;

    public bool debugModeOn = false;

    public UnityEvent OnBadSegmentTrigger;

    public UnityEvent OnGoodSegmentTrigger;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Good
        OnGoodSegmentTrigger.AddListener(() => {
            Services.instance.Get<ScoreManager>()?.AddScore(1);
            Services.instance.Get<AudioManager>()?.PlaySound(AudioManager.SoundEventType.Whoosh);
        });

        // Bad
        OnBadSegmentTrigger.AddListener(() => {
            Services.instance.Get<AudioManager>()?.PlaySound(AudioManager.SoundEventType.ObstacleHit);
        });
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
