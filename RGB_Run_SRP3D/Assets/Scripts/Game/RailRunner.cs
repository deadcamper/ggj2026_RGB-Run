using UnityEngine;
using UnityEngine.Splines;

public class RailRunner : MonoBehaviour
{
    [SerializeField]
    private SplineAnimate splineAnimator;

    [SerializeField]
    [UnityEngine.Serialization.FormerlySerializedAs("railSystem")]
    private RailsSegment activeRailSegment;

    private SplineContainer activeRailTrack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SplineContainer rail = activeRailSegment.GetMiddleRailTrack();
        SetTrackAndReset(rail);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void JumpToTrackByIndex(int index)
    {
        SplineContainer newRail = activeRailSegment.GetRailTrack(index);

        if (newRail == activeRailTrack)
            return;

        SetTrackAndJump(newRail);
    }

    public void JumpToTrackByOffset(int signum)
    {
        int prevRailNum = activeRailSegment.GetIndexForRailTrack(activeRailTrack);
        int railNum = prevRailNum + signum;

        // Debug log
        //Debug.Log($"{prevRailNum} -> {railNum}");

        SplineContainer newRail = activeRailSegment.GetRailTrack(railNum);

        if (newRail == activeRailTrack)
            return;

        SetTrackAndJump(newRail);
    }

    private void SetTrackAndReset(SplineContainer rail)
    {
        // Zero out positioning, though this may not be necessary.
        var localPosition = rail.EvaluatePosition(0f);
        Debug.Log(localPosition);
        var worldPosition = rail.transform.InverseTransformPoint(localPosition);
        gameObject.transform.position = worldPosition;

        splineAnimator.Container = rail;
        splineAnimator.NormalizedTime = 0;
        splineAnimator.Restart(true);

        activeRailTrack = rail;
    }

    private void SetTrackAndJump(SplineContainer rail)
    {
        Vector3 position = transform.position;

        float prevTime = splineAnimator.NormalizedTime;

        Vector3 localSplinePoint = rail.transform.InverseTransformPoint(position);

        SplineUtility.GetNearestPoint(rail.Spline, localSplinePoint, out Unity.Mathematics.float3 nearest, out float normalisedCurvePos,
            SplineUtility.PickResolutionMax, SplineUtility.PickResolutionMax);

        // Debugging log
        //Debug.Log($"{position} -> {localSplinePoint} -> {nearest} ({prevTime} -> {normalisedCurvePos}) ");

        splineAnimator.Container = rail;

        float extraNormalCurvePos = Mathf.Clamp01(normalisedCurvePos); // curve position can be out of bounds if past the point.

        splineAnimator.NormalizedTime = extraNormalCurvePos;

        activeRailTrack = rail;
    }
}
