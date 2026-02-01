using SevenSegmentDisplay;
using UnityEngine;

public abstract class AbstractObstacleSpawner : MonoBehaviour
{
    public abstract void Spawn(Obstacle prefab, Digits digits);
}
