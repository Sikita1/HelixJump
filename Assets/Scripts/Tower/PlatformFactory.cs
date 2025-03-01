using UnityEngine;

class PlatformFactory
{
    public T Create<T>(T prefab, ref Vector3 spawnPosition, float randomY, Transform parent) where T : Platform
    {
        T platform = GameObject.Instantiate(prefab, spawnPosition, Quaternion.Euler(0, randomY, 0), parent);
        spawnPosition.y -= 1;

        return platform;
    }
}

