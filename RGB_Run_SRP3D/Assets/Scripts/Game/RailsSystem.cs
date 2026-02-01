using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

// Keeps track of available rails and can move the player from one rail to another.
public class RailsSystem : MonoBehaviour
{

    [SerializeField]
    private List<SplineContainer> railPaths;

    public int GetIndexForRail(SplineContainer rail)
    {
        int index = railPaths.IndexOf(rail);
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
    public SplineContainer GetRail(int index)
    {
        index = Mathf.Clamp(index, 0, railPaths.Count - 1);
        return railPaths[index];
    }

    public SplineContainer GetMiddleRail()
    {
        int mid = railPaths.Count / 2;
        return railPaths[mid];
    }

}
