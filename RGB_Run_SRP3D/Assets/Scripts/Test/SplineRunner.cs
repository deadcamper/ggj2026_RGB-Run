using UnityEngine;
using UnityEngine.Splines;

public class SplineRunner : MonoBehaviour
{
    public SplineAnimate runner;

    public SplineContainer[] splinePaths;

    [Range(0, 8)]
    public int pathIndex; // Allows for switch

    private int activePathIndex = -1;
    private SplineContainer activePath;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwapSpline(pathIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (pathIndex != activePathIndex)
        {
            SwapSpline(pathIndex);
        }
    }

    private void SwapSpline(int newIndex)
    {
        newIndex = Mathf.Clamp(newIndex, 0, splinePaths.Length-1);

        Vector3 position = runner.transform.position;

        float prevTime = runner.NormalizedTime;

        SplineContainer nextSpline = splinePaths[newIndex];

        Vector3 localSplinePoint = nextSpline.transform.InverseTransformPoint(position);

        SplineUtility.GetNearestPoint(nextSpline.Spline, localSplinePoint, out Unity.Mathematics.float3 nearest, out float normalisedCurvePos,
            SplineUtility.PickResolutionMax, SplineUtility.PickResolutionMax);

        // Debugging code
        // Debug.Log($"{position} -> {localSplinePoint} -> {nearest} ({prevTime} -> {normalisedCurvePos}) ");

        runner.Container = nextSpline;

        float extraNormalCurvePos = Mathf.Clamp01(normalisedCurvePos); // curve position can be out of bounds if past the point.

        runner.NormalizedTime = extraNormalCurvePos;

        activePathIndex = pathIndex;
    }
}
