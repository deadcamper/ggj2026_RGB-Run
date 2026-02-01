using UnityEngine;
using UnityEngine.Splines;

public class RailRunner : MonoBehaviour
{
    [SerializeField]
    private SplineAnimate splineAnimator;

    [SerializeField]
    private RailsSystem railSystem;

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

    public void SetRailAndReset(SplineContainer rail)
    {
        var localPosition = rail.EvaluatePosition(0f);
        Debug.Log(localPosition);
        var worldPosition = rail.transform.InverseTransformPoint(localPosition);
        gameObject.transform.position = worldPosition;

        splineAnimator.Container = rail;
        splineAnimator.NormalizedTime = 0;
        splineAnimator.Restart(true);
    }
}
