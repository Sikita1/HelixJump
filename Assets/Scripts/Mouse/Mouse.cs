using System;
using UnityEngine;
using DG.Tweening;

public class Mouse : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private MouseJumper _jumper;
    [SerializeField] private Vector3 _target;
    [SerializeField] private float _slewingSpeed;

    private float _forceSuperAttack = 1.3f;

    public event Action GameOver;
    public event Action GameWin;

    private float _currentPassedPlatform = 0f;

    public event Action<float> ChangePassPlatform;

    public bool IsAttack { get; private set; }

    private void Start()
    {
        _trailRenderer.gameObject.SetActive(false);
        ChangePassPlatform?.Invoke(_currentPassedPlatform);
    }

    private void Update()
    {
        GoOnAttack();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlatformSegment platformSegment))
        {
            other.GetComponentInParent<Platform>().Break();
            _currentPassedPlatform++;
            ChangePassPlatform?.Invoke(_currentPassedPlatform);
        }

        if (other.gameObject.TryGetComponent(out EarthSegment earthSegment))
        {
            earthSegment.Hide();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out EarthSegment earthSegment))
        {
            earthSegment.Break();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        FinishPlatform finishPlatform = collision.gameObject.GetComponentInParent<FinishPlatform>();

        if (finishPlatform != null)
        {
            GameWin?.Invoke();
        }

        if (collision.gameObject.TryGetComponent(out DangerSegment dangerSegment))
        {
            GameOver?.Invoke();

            dangerSegment.ChangeStyle();
        }

        if (collision.gameObject.TryGetComponent(out GlassSegment glassSegment))
        {
            if (IsAttack)
            {
                glassSegment.Crash();
            }
        }

        if (collision.gameObject.TryGetComponent(out PlatformSegment platformSegment))
        {
            _jumper.Jump(1f);
        }

        if (IsAttack)
            FinishAttack();
    }

    public bool Attack()
    {
        _jumper.Jump(_forceSuperAttack);
        _trailRenderer.gameObject.SetActive(true);

        return IsAttack = true;
    }

    public bool FinishAttack()
    {
        _trailRenderer.gameObject.SetActive(false);

        return IsAttack = false;
    }

    private void GoOnAttack()
    {
        if (IsAttack)
            transform.DORotate(_target, 0.5f, RotateMode.FastBeyond360);
    }
}
