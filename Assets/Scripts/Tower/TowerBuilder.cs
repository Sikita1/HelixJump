using UnityEngine;

public class TowerBuilder
{
    public SpawnPlatform SpawnPlatform { get; private set; }
    public FinishPlatform FinishPlatform { get; private set; }
    public Ball Ball { get; private set; }
    public Beam Beam { get; private set; }

    public void Build(Transform parent, BallTracking ballTracking)
    {
        TowerSO towerSO = Resources.Load<TowerSO>("TowerSO");
        PlatformSO platformSO = Resources.Load<PlatformSO>("PlatformSO");

        PlatformFactory platformSpawner = new PlatformFactory();
        BeamFactory beamSpawner = new BeamFactory();

        Beam = beamSpawner.Create(towerSO.Beam, parent, towerSO.BeamScaleY);

        Vector3 spawnPosition = Beam.transform.position;
        spawnPosition.y += Beam.transform.localScale.y - towerSO.PlatformAdditionalScale;

        SpawnPlatform = platformSpawner.Create(platformSO.SpawnPlatform,
                                                  ref spawnPosition,
                                                  Beam.transform.parent);

        Ball = SpawnPlatform.CreateBall();
        ballTracking.Initialize(Ball, Beam);

        for (int i = 0; i < towerSO.PlatformCount; i++)
        {
            platformSpawner.Create(platformSO.Platforms[Random.Range(0, platformSO.Platforms.Length)],
                                  ref spawnPosition,
                                  Beam.transform.parent);
        }

        FinishPlatform = platformSpawner.Create(platformSO.FinishPlatform,
                                          ref spawnPosition,
                                          Beam.transform.parent);
    }
}