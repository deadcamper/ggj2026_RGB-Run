using UnityEngine;
using UnityEngine.Splines;

public class RailRunner : MonoBehaviour
{
    [SerializeField]
    private SplineAnimate splineAnimator;

    [SerializeField]
    private RailsSegment railSystem;

    private SplineContainer activeRail;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SplineContainer rail = railSystem.GetMiddleRail();
        SetRailAndReset(rail);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void JumpToRailByIndex(int index)
    {
        SplineContainer newRail = railSystem.GetRail(index);

        if (newRail == activeRail)
            return;

        SetRailAndJump(newRail);
    }

    public void JumpToRailByOffset(int signum)
    {
        int prevRailNum = railSystem.GetIndexForRail(activeRail);
        int railNum = prevRailNum + signum;

        // Debug log
        //Debug.Log($"{prevRailNum} -> {railNum}");

        SplineContainer newRail = railSystem.GetRail(railNum);

        if (newRail == activeRail)
            return;

        SetRailAndJump(newRail);
    }

    private void SetRailAndReset(SplineContainer rail)
    {
        // Zero out positioning, though this may not be necessary.
        var localPosition = rail.EvaluatePosition(0f);
        Debug.Log(localPosition);
        var worldPosition = rail.transform.InverseTransformPoint(localPosition);
        gameObject.transform.position = worldPosition;

        splineAnimator.Container = rail;
        splineAnimator.NormalizedTime = 0;
        splineAnimator.Restart(true);

        activeRail = rail;
    }

    private void SetRailAndJump(SplineContainer rail)
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

        activeRail = rail;
    }
}
