using R3;
using UnityEngine;

public class TestRotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            var euler = transform.rotation;
            var newRot = Quaternion.Euler(8f * Time.time, euler.y, euler.z);
            transform.rotation = newRot;
        }).AddTo(this);    
    }
}
