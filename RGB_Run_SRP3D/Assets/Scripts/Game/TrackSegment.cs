using SevenSegmentDisplay;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    public event Action OnFinished;

    [SerializeField] private Obstacle obstaclePrefab;

    public Transform NextSegmentAnchor => nextSegmentAnchor;
    [SerializeField] private Transform nextSegmentAnchor;

    public void Setup(LinkedListNode<TrackSegment> node, Digits digits)
    {
        var priorSegment = node.Previous?.Value;
        if (priorSegment != null)
        {
            transform.SetPositionAndRotation(priorSegment.NextSegmentAnchor.position, priorSegment.NextSegmentAnchor.rotation);
        }
        foreach(var obstacleSpawner in GetComponentsInChildren<AbstractObstacleSpawner>())
        {
            obstacleSpawner.Spawn(obstaclePrefab, digits);
        }
    }
}
