using UnityEngine;

public class TestRotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var euler = transform.rotation;
        var newRot = Quaternion.Euler(8f * Time.time, euler.y, euler.z);
        transform.rotation = newRot;
    }
}
