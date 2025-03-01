using UnityEngine;

[CreateAssetMenu(fileName = "PlatformSO", menuName = "ScriptableObject/PlatformSO")]
public class PlatformSO : ScriptableObject
{
    [field: SerializeField] public FinishPlatform FinishPlatform { get; private set; }
    [field: SerializeField] public SpawnPlatform SpawnPlatform { get; private set; }
    [field: SerializeField] public Platform[] Platforms { get; private set; }
}

