using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Just useful for propagating messages from collider to whatever needs to listen.
/// </summary>
public class CollisionListener : MonoBehaviour
{
    public UnityEvent<Collision> EventOnCollisionEnter;

    public UnityEvent<Collider> EventOnTriggerEnter;


    void OnCollisionEnter(Collision collision)
    {
        EventOnCollisionEnter.Invoke(collision);
    }

    void OnTriggerEnter(Collider collider)
    {
        EventOnTriggerEnter.Invoke(collider);
    }

}
