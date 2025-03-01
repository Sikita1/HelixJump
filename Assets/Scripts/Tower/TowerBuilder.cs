using UnityEngine;

public class TowerBuilder
{
    private const string SaveKeyScene = "saveScene";

    private float _maxRandomY = 360f;

    public SpawnPlatform SpawnPlatform { get; private set; }
    public FinishPlatform FinishPlatform { get; private set; }
    public Mouse Mouse { get; private set; }
    public Beam Beam { get; private set; }
    public int MaxLevelIndex { get; private set; } = 0;
    
    private float _randomY => Random.Range(0, _maxRandomY);

    public void Build(Transform parent, MouseTracking mouseTracking)
    {
        TowerSOCollection towerSOCollection = Resources.Load<TowerSOCollection>("TowerSOCollection");
        TowerSO towerSO = towerSOCollection.GetLevel(LoadScene());
        //PlatformSO platformSO = Resources.Load<PlatformSO>("PlatformSO");
        PlatformSO platformSO = towerSO.PlatformSO;

        PlatformFactory platformSpawner = new PlatformFactory();
        BeamFactory beamSpawner = new BeamFactory();

        towerSOCollection.GetLevel(LoadScene());

        Beam = beamSpawner.Create(towerSO.Beam, parent, towerSO.BeamScaleY);
        Beam.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(1, towerSO.BeamScaleY);

        Vector3 spawnPosition = Beam.transform.position;
        spawnPosition.y += Beam.transform.localScale.y - towerSO.PlatformAdditionalScale;

        SpawnPlatform = platformSpawner.Create(platformSO.SpawnPlatform,
                                               ref spawnPosition,
                                               0,
                                               Beam.transform.parent);

        MaxLevelIndex++;

        Mouse = SpawnPlatform.CreateBall();
        mouseTracking.Initialize(Mouse, Beam);

        for (int i = 0; i < towerSO.PlatformCount; i++)
        {
            platformSpawner.Create(platformSO.Platforms[Random.Range(0, platformSO.Platforms.Length)],
                                   ref spawnPosition,
                                   _randomY,
                                   Beam.transform.parent);

            MaxLevelIndex++;
        }

        FinishPlatform = platformSpawner.Create(platformSO.FinishPlatform,
                                                ref spawnPosition,
                                                _randomY,
                                                Beam.transform.parent);
    }

    private int LoadScene()
    {
        SaveData.SceneController data = SaveManager.Load<SaveData.SceneController>(SaveKeyScene);
        return data.CurrentScene;
    }
}