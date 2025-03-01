using UnityEngine;

public class MouseTracking : MonoBehaviour
{
    [SerializeField] private Vector3 _directionOffset;
    [SerializeField] private float _lenght;

    private Vector3 _cameraPosition;
    private Vector3 _minimusMousePosition;
    
    private Mouse _mouse;
    private Beam _beam;

    private void Update()
    {
        if (_mouse != null)
        {
            if (_mouse.transform.position.y < _minimusMousePosition.y)
            {
                TrackBall();
                _minimusMousePosition = _mouse.transform.position;
            }
        }
    }

    public void Initialize(Mouse mouse, Beam beam)
    {
        _mouse = mouse;
        _beam = beam;

        _cameraPosition = _mouse.transform.position;
        _minimusMousePosition = _mouse.transform.position;

        TrackBall();
    }

    private void TrackBall()
    {
        Vector3 beamPosition = _beam.transform.position;
        beamPosition.y = _mouse.transform.position.y;

        _cameraPosition = _mouse.transform.position;
        Vector3 direction = (beamPosition - _mouse.transform.position).normalized + _directionOffset;
        _cameraPosition -= direction * _lenght;
        transform.position = _cameraPosition;
        transform.LookAt(_mouse.transform);
    }
}
