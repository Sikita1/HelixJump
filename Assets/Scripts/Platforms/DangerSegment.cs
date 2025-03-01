using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class DangerSegment : MonoBehaviour
{
    [SerializeField] private Thorn _thorn;
    [SerializeField] private Material _materialGrass;

    private MeshRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void ChangeStyle()
    {
        Destroy(_thorn.gameObject);
        _renderer.material = _materialGrass;
        Destroy(this);
    }
}

