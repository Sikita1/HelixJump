using UnityEngine;

[RequireComponent(typeof(Mouse))]
public class MouseRotator : MonoBehaviour
{
    [SerializeField] private float _leftRotation;
    [SerializeField] private float _rightRotation;
    [SerializeField] private float _rotationSpeed = 10f;

    private Quaternion _targetRotation;
    private float _swipeDownY = -1.5f;

    private Mouse _mouse;

    private void Awake()
    {
        _mouse = GetComponent<Mouse>();
    }

    void Start()
    {
        _targetRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxisRaw("Mouse X");
            float mouseY = Input.GetAxisRaw("Mouse Y");

            if (mouseX != 0)
            {
                if (mouseX > 0)
                    _targetRotation = Quaternion.Euler(0, _rightRotation, 0);
                else if (mouseX < 0)
                    _targetRotation = Quaternion.Euler(0, _leftRotation, 0);

                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _rotationSpeed);
            }

            if(mouseY != 0)
                if(mouseY < _swipeDownY)
                    _mouse.Attack();
        }
    }
}
