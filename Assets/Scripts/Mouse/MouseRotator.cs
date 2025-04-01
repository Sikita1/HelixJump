using UnityEngine;

[RequireComponent(typeof(Mouse))]
public class MouseRotator : MonoBehaviour
{
    [SerializeField] private float _leftRotation;
    [SerializeField] private float _rightRotation;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _swipeDownThreshold = 100f;

    private Quaternion _targetRotation;
    private Mouse _mouse;
    private bool _isMobilePlatform;
    private Vector2 _touchStartPosition;

    private float _swipeDownY = -1.5f;

    private void Awake()
    {
        _mouse = GetComponent<Mouse>();
        _isMobilePlatform = Application.isMobilePlatform;
    }

    void Start()
    {
        _targetRotation = transform.rotation;
    }

    void Update()
    {
        if (_isMobilePlatform)
        {
            HandleMobileInput();
        }
        else
        {
            HandleDesktopInput();
        }
    }

    private void HandleDesktopInput()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxisRaw("Mouse X");
            float mouseY = Input.GetAxisRaw("Mouse Y");

            ProcessRotation(mouseX);

            if (mouseY < _swipeDownY)
            {
                _mouse.Attack();
            }
        }
    }

    private void HandleMobileInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _touchStartPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                    float mouseX = touch.deltaPosition.x / Screen.dpi * 2f;
                    ProcessRotation(-mouseX);
                    break;

                case TouchPhase.Ended:
                    float swipeDistance = _touchStartPosition.y - touch.position.y;

                    if (swipeDistance > _swipeDownThreshold)
                        _mouse.Attack();

                    break;
            }
        }
    }

    private void ProcessRotation(float inputX)
    {
        if (inputX != 0)
        {
            if (inputX > 0)
                _targetRotation = Quaternion.Euler(0, _rightRotation, 0);
            else if (inputX < 0)
                _targetRotation = Quaternion.Euler(0, _leftRotation, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _rotationSpeed);
        }
    }
}
