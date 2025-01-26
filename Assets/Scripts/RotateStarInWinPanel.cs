using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RotateStarInWinPanel : MonoBehaviour
{
    [SerializeField] private float _slewingSpeed;
    
    private RectTransform _transform;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        _transform.Rotate(0,0, _slewingSpeed);
    }
}
