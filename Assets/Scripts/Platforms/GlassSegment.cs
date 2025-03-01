using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class GlassSegment : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private MeshRenderer _renderer;

    private MeshCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<MeshCollider>();
        _renderer = GetComponent<MeshRenderer>();
    }

    public void Crash()
    {
        _renderer.gameObject.SetActive(false);
        _collider.enabled = false;

        _particleSystem.Play();
    }
}
