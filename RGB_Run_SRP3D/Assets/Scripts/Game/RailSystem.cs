using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class RailSystem : MonoBehaviour
{
    public List<RailsSegment> segmentTemplates;

    public RailsSegment startingSegment;

    private RailsSegment futureSegment;

    private RailsSegment activeSegment;

    private RailsSegment previousSegment; // this list adds and drops as the ride proceeds


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeSegment = startingSegment;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RequestNextSegment()
    {

    }

    private void ConnectSegments(RailsSegment beforeSegment, RailsSegment afterSegment)
    {
        SplineContainer beforeMiddle = beforeSegment.GetMiddleRailTrack();
        SplineContainer afterMiddle = beforeSegment.GetMiddleRailTrack();

        Unity.Mathematics.float3 beforePoint = beforeMiddle.EvaluatePosition(1f);
    }
}
