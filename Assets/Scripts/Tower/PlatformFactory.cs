using UnityEngine;

class PlatformFactory
{
    public T Create<T>(T prefab, ref Vector3 spawnPosition, Transform parent) where T : Platform
    {
        T platform = GameObject.Instantiate(prefab, spawnPosition, Quaternion.Euler(0, Random.Range(0, 360), 0), parent);
        spawnPosition.y -= 1;

        return platform;
    }
}

