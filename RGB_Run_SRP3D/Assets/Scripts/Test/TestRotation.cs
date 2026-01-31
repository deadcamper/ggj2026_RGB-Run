using R3;
using UnityEngine;

public class TestRotation : MonoBehaviour
{
    public float speed = 8f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            var euler = transform.rotation;
            transform.Rotate(new Vector3(speed * Time.deltaTime, euler.y, euler.z));
        }).AddTo(this);    
    }

    public void SetRotationSpeed(float speed)
    {
        this.speed = speed;
    }
}
