using UnityEngine;

[CreateAssetMenu(fileName = "TowerSOCollection", menuName = "ScriptableObject/TowerSOCollection")]
public class TowerSOCollection : ScriptableObject
{
    [SerializeField] private TowerSO[] _towerSOs;

    public TowerSO GetLevel(int currentLevel)
    {
        foreach (var tower in _towerSOs)
            if (tower.Level == currentLevel)
                return tower;

        return null;
    }
}
