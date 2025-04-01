using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TowerRotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 300f;
    [SerializeField] private float _rotationSpeedMobile = 2f;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxisRaw("Mouse X");
            transform.Rotate(transform.position.x,
                             -mouseX * _rotationSpeed * Time.deltaTime,
                             transform.position.z);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            float xDeltaPosition = Input.GetTouch(0).deltaPosition.x;
            transform.Rotate(transform.position.x,
                             xDeltaPosition * _rotationSpeedMobile * Time.deltaTime,
                             transform.position.z);
        }
    }
}
