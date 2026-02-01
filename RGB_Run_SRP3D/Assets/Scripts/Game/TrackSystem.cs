using SevenSegmentDisplay;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

public class TrackSystem : MonoBehaviour
{
    [SerializeField] private TrackSegment[] trackSegmentPrefabs;

    public TrackSegment CurrentSegment => track.First.Value;
    public TrackSegment LastSegment => track.Last.Value;

    private LinkedList<TrackSegment> track = new();
    private LinkedList<TrackSegment> oldTrack = new();

    [SerializeField] private int trackSegmentCount = 5;

    [SerializeField] private int oldTrackSegmentCount = 1;

    [SerializeField] private int railCount = 3;

    private float distance;

    //private int rail;

    //private IDisposable keypressListener;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //rail = railCount / 2;
        /*
        keypressListener =  InputSystem.onEvent
            .Where(e => e.HasButtonPress())
            .Call(eventPtr =>
            {
                foreach (var key in eventPtr.GetAllButtonPresses().OfType<KeyControl>())
                {
                    HandleKeyPress(key.keyCode);
                }
            });
        */
        SpawnMoreTrack();
        PopTrackSegment();// so we have some behind us

    }

    void OnDestroy()
    {
        //keypressListener?.Dispose();
    }

    /*
    private void HandleKeyPress(Key key)
    {
        switch(key)
        {
            case Key.A:
                if (rail > 0)
                    rail--;
                break;
            case Key.D:
                if (rail < railCount - 1) 
                    rail++;
                break;
        }
    }
    */

    public TrackSegment RequestNewTrack()
    {
        SpawnMoreTrack();
        PopTrackSegment();

        return CurrentSegment;
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
        var nextSegment = track.First.Next.Value;
        nextSegment.transform.parent = null;
        segment.transform.parent = nextSegment.transform;
        track.RemoveFirst();
        oldTrack.AddLast(segment);
        while (oldTrack.Count > oldTrackSegmentCount)
        {
            var tooOldSegment = oldTrack.First.Value;
            oldTrack.RemoveFirst();
            Destroy(tooOldSegment.gameObject);
        }
        SpawnMoreTrack();
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
    }



    private void Update()
    {
        /*
        distance += Time.deltaTime * .25f;
        while (distance > 1)
        {
            PopTrackSegment();
            distance -= 1;
        }
        SpawnMoreTrack();
        var segment = CurrentSegment;
        if (segment != null)
        {
            segment.transform.position -= segment.RailsSegment.GetWorldSpacePositionOnRail(rail, distance);
        }
        */
    }
}
