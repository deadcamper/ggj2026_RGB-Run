using R3;
using UnityEngine;
using UnityEngine.Splines;

public class PacingController : MonoBehaviour
{
    public SplineAnimate splineAnimate;

    public TrackSystem trackSystem;
    [SerializeField] private float speed;

    private void Start()
    {
        trackSystem = FindAnyObjectByType<TrackSystem>();
        trackSystem.OnSegmentStarted += TrackSystem_OnSegmentStarted;
        TrackSystem_OnSegmentStarted();
    }

    private void OnDestroy()
    {
        if (trackSystem != null)
        {
            trackSystem.OnSegmentStarted -= TrackSystem_OnSegmentStarted;
        }
    }

    private void TrackSystem_OnSegmentStarted()
    {
        var score = Services.instance.Get<GameStateManager>().Score.CurrentValue;
        splineAnimate.MaxSpeed = (1+Mathf.Log10(score+1)) * speed;
    }
}
