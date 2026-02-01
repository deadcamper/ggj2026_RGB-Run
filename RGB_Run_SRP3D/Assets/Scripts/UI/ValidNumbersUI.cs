using UnityEngine;
using UnityEngine.Pool;
using SevenSegmentDisplay;


public class ValidNumbersUI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI validNumbersText;

    TrackSystem trackSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trackSystem = FindAnyObjectByType<TrackSystem>() ?? throw new System.Exception();
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
        var numbers = ListPool<int>.Get();
        trackSystem.CurrentSegment.Digits.GetIndividualNumbers(numbers);
        validNumbersText.text = string.Join(" ", numbers);
        ListPool<int>.Release(numbers);
    }
}
