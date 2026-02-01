using SevenSegmentDisplay;
using System.Collections.Generic;
using UnityEngine;

public class TrackSystem : MonoBehaviour
{
    [SerializeField] private TrackSegment[] trackSegmentPrefabs;

    public TrackSegment CurrentSegment => track.First.Value;
    public TrackSegment LastSegment => track.Last.Value;

    private LinkedList<TrackSegment> track = new();
    private LinkedList<TrackSegment> oldTrack = new();

    [SerializeField] private int trackSegmentCount;

    [SerializeField] private int oldTrackSegmentCount = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SpawnMoreTrack();
    }


    private void SpawnMoreTrack()
    {
        while (track.Count < trackSegmentCount)
        {
            AddTrackSegment();
        }
    }

    private void PopTrackSegment()
    {
        var segment = track.First.Value;
        track.RemoveFirst();
        segment.transform.parent = track.First?.Value.transform;
        oldTrack.AddLast(segment);
        while (oldTrack.Count > oldTrackSegmentCount)
        {
            var tooOldSegment = oldTrack.First.Value;
            oldTrack.RemoveFirst();
            Destroy(tooOldSegment.gameObject);
        }
        SetUpCurrentTrackSegment();
        SpawnMoreTrack();

    }

    private void SetUpCurrentTrackSegment()
    {
        CurrentSegment.OnFinished -= PopTrackSegment;
        CurrentSegment.OnFinished += PopTrackSegment;
    }

    private void AddTrackSegment()
    {
        var priorSegment = track.Last?.Value;
        var prefab = trackSegmentPrefabs[UnityEngine.Random.Range(0, trackSegmentPrefabs.Length)];
        var segment = Instantiate(prefab, priorSegment?.transform);

        if(priorSegment != null)
        {
            segment.transform.localPosition = priorSegment.NextSegmentAnchor.localPosition;
            segment.transform.localRotation = priorSegment.NextSegmentAnchor.localRotation;
        }

        track.AddLast(segment);
        var node = track.Last;

        segment.Setup(node, Digits.One | Digits.Two | Digits.Three);

        if (node == track.First)
        {
            SetUpCurrentTrackSegment();
        }
    }
}
