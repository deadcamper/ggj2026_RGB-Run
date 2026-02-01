using SevenSegmentDisplay;

public class SingleObstacleSpawner : AbstractObstacleSpawner
{
    public bool passable = false;
    public override void Spawn(Obstacle prefab, Digits digits)
    {
        var obstacle = Instantiate(prefab, transform);
        obstacle.transform.SetPositionAndRotation(transform.position, transform.rotation);
        obstacle.Set(passable, digits);
    }
}
