using UnityEngine;

public class RGBRunnerPlayer : MonoBehaviour
{
    public RailRunner railRunner;

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
        railRunner.JumpToRailByIndex(0);
    }

    void OnActionTwo()
    {
        railRunner.JumpToRailByIndex(1);
    }

    void OnActionThree()
    {
        railRunner.JumpToRailByIndex(2);
    }
    #endregion

    void OnMoveLeft()
    {
        railRunner.JumpToRailByOffset(-1);
    }

    void OnMoveRight()
    {
        railRunner.JumpToRailByOffset(1);
    }


}
