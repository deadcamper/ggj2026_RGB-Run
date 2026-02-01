using UnityEngine;
using UnityEngine.Splines;

public class RGBRunnerPlayer : MonoBehaviour
{
    public RailsSystem railSystem;
    public RailRunner railRunner;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SplineContainer rail = railSystem.GetMiddleRail();
        railRunner.SetRailAndReset(rail);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
