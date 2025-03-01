using UnityEngine;

public class SpawnPlatform : Platform
{
    [SerializeField] private Mouse _mouse;
    [SerializeField] private Transform _spawnPoint;

    public Mouse CreateBall() =>
        Create(_mouse, _spawnPoint.position);

    private Mouse Create(Mouse ball, Vector3 position) =>
        Instantiate(ball, position, Quaternion.identity);
}
