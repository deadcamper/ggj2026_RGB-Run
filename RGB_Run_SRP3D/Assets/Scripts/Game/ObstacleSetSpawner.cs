using SevenSegmentDisplay;
using UnityEngine;

public class ObstacleSetSpawner : AbstractObstacleSpawner
{
    [SerializeField] Transform[] locations;

    public override void Spawn(Obstacle prefab, Digits digits)
    {
        int safeIndex = UnityEngine.Random.Range(0, locations.Length);
        for (int i = 0; i < locations.Length; i++)
        {
            var obstacleLocation = locations[i];
            var obstacle = Instantiate(prefab, transform);
            transform.SetPositionAndRotation(obstacleLocation.position, obstacleLocation.rotation);
            obstacle.Set(i == safeIndex, digits);
        }
    }
}
