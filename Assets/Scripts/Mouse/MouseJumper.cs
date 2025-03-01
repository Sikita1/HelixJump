using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MouseJumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Jump(float force)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(Vector3.up * _jumpForce * force, ForceMode.Impulse);
    }
}
