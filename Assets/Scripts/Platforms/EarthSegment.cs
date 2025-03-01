using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class EarthSegment : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private ParticleSystem _particleSystem;

    public int IsBroken = 0;

    private MeshRenderer _renderer;

    private MeshCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<MeshCollider>();
        _renderer = GetComponent<MeshRenderer>();
        _sprite.gameObject.SetActive(false);
    }

    public void Break()
    {
        if (IsBroken == 0)
        {
            _sprite.gameObject.SetActive(true);
            _collider.isTrigger = true;
        }

        IsBroken++;
    }

    public void Hide()
    {
        if (IsBroken == 1)
        {
            _renderer.gameObject.SetActive(false);
            _particleSystem.Play();
        }
    }
}
