using UnityEngine;

public class RGBRunnerPlayer : MonoBehaviour
{
    public RailRunner railRunner;

    public RailsSegment startingSegment;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Debug buttons
    void OnActionOne()
    {
        railRunner.JumpToTrackByIndex(0);
    }

    void OnActionTwo()
    {
        railRunner.JumpToTrackByIndex(1);
    }

    void OnActionThree()
    {
        railRunner.JumpToTrackByIndex(2);
    }
    #endregion

    void OnMoveLeft()
    {
        railRunner.JumpToTrackByOffset(-1);
    }

    void OnMoveRight()
    {
        railRunner.JumpToTrackByOffset(1);
    }


}
