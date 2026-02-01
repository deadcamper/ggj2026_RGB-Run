using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

// Keeps track of available rails and can move the player from one rail to another.
public class RailsSegment : MonoBehaviour
{

    [SerializeField]
    [UnityEngine.Serialization.FormerlySerializedAs("railPaths")]
    private List<SplineContainer> railTracks;

    public int GetIndexForRailTrack(SplineContainer rail)
    {
        int index = railTracks.IndexOf(rail);
        if (index == -1)
        {
            Debug.LogError($"Rail index not found for rail {rail}.");
        }
        return index;
    }

    /// <summary>
    /// Used when trying to leap from rail to rail.
    /// 
    /// Returns rail by index, BOUNDED by size of paths.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public SplineContainer GetRailTrack(int index)
    {
        index = Mathf.Clamp(index, 0, railTracks.Count - 1);
        return railTracks[index];
    }

    public SplineContainer GetMiddleRailTrack()
    {
        int mid = railTracks.Count / 2;
        return railTracks[mid];
    }

}
