using UnityEngine;

public class BallTracking : MonoBehaviour
{
    [SerializeField] private Vector3 _directionOffset;
    [SerializeField] private float _lenght;

    private Vector3 _cameraPosition;
    private Vector3 _minimusBallPosition;
    
    private Ball _ball;
    private Beam _beam;

    private void Update()
    {
        if (_ball != null)
        {
            if (_ball.transform.position.y < _minimusBallPosition.y)
            {
                TrackBall();
                _minimusBallPosition = _ball.transform.position;
            }
        }
    }

    public void Initialize(Ball ball, Beam beam)
    {
        _ball = ball;
        _beam = beam;

        _cameraPosition = _ball.transform.position;
        _minimusBallPosition = _ball.transform.position;

        TrackBall();
    }

    private void TrackBall()
    {
        Vector3 beamPosition = _beam.transform.position;
        beamPosition.y = _ball.transform.position.y;

        _cameraPosition = _ball.transform.position;
        Vector3 direction = (beamPosition - _ball.transform.position).normalized + _directionOffset;
        _cameraPosition -= direction * _lenght;
        transform.position = _cameraPosition;
        transform.LookAt(_ball.transform);
    }
}
