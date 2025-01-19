using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class MonoBehaviorExtension
    {
        public static T GetOrAdd<T>(this Transform transform) where T : Component
        {
            if (transform.TryGetComponent<T>(out T parametr))
                return parametr;

            return transform.gameObject.AddComponent<T>();
        }
    }
}
