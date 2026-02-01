using SevenSegmentDisplay;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RailsSegment))]
public class TrackSegment : MonoBehaviour
{
    public RailsSegment RailsSegment => _RailsSegment ?? (_RailsSegment = GetComponent<RailsSegment>());
    private RailsSegment _RailsSegment;

    [SerializeField] private Obstacle obstaclePrefab;

    public Transform NextSegmentAnchor => nextSegmentAnchor;
    [SerializeField] private Transform nextSegmentAnchor;

    public Digits Digits { get; private set; }

    public void Setup(LinkedListNode<TrackSegment> node, Digits digits, bool obstacleEnabled)
    {
        Digits = digits;
        var priorSegment = node.Previous?.Value;
        if (priorSegment != null)
        {
            transform.SetPositionAndRotation(priorSegment.NextSegmentAnchor.position, priorSegment.NextSegmentAnchor.rotation);
        }
        if (obstacleEnabled)
        {
            foreach (var obstacleSpawner in GetComponentsInChildren<AbstractObstacleSpawner>())
            {
                obstacleSpawner.Spawn(obstaclePrefab, digits);
            }
        }
    }
}
