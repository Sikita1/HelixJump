using UnityEngine;

public class BallTracking : MonoBehaviour
{
    [SerializeField] private Vector3 _directionOffset;
    [SerializeField] private float _lenght;

    [SerializeField] private Ball _ball;
    [SerializeField] private Beam _beam;

    private Vector3 _cameraPosition;
    private Vector3 _minimusBallPosition;

    private void Start()
    {
        _ball = FindObjectOfType<Ball>();
        _beam = FindObjectOfType<Beam>();

        //_ball = GetComponent<Ball>();
        //_beam = GetComponent<Beam>();

        _cameraPosition = _ball.transform.position;
        _minimusBallPosition = _ball.transform.position;

        TrackBall();
    }

    private void Update()
    {
        if(_ball.transform.position.y < _minimusBallPosition.y)
        {
            TrackBall();
            _minimusBallPosition = _ball.transform.position;
        }
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
