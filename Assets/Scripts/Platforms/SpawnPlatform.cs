using UnityEngine;

public class SpawnPlatform : Platform
{
    [SerializeField] private Ball _ball;
    [SerializeField] private Transform _spawnPoint;

    public Ball CreateBall() =>
        Create(_ball, _spawnPoint.position);

    private Ball Create(Ball ball, Vector3 position) =>
        Instantiate(ball, position, Quaternion.identity);
}
