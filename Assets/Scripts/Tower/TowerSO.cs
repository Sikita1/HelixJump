using UnityEngine;

[CreateAssetMenu(fileName = "TowerSO", menuName = "ScriptableObject/TowersSO/TowerSO")]
public class TowerSO : ScriptableObject
{
    [field: SerializeField] public Beam Beam { get; private set; }
    [field: SerializeField] public float PlatformAdditionalScale { get; private set; }
    [field: SerializeField] public float PlatformCount { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public PlatformSO PlatformSO { get; private set; }

    private float _startAndFinishAdditionalScale = 0.5f;


    public float BeamScaleY =>
        PlatformCount / 2f + _startAndFinishAdditionalScale + PlatformAdditionalScale / 2f;
}

