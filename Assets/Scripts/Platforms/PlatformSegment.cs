using UnityEngine;
using DG.Tweening;
using Assets.Scripts.Extensions;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class PlatformSegment : MonoBehaviour
{
    private MeshRenderer _renderer;
    private WaitForSeconds _wait;
    private float _delay = 0.7f;

    private Coroutine _coroutine;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _wait = new WaitForSeconds(_delay);
        //_renderer = transform.GetOrAdd<MeshRenderer>();
    }

    public void Bounce(float force, Vector3 centre, float radius)
    {
        if(TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.isKinematic = false;
            rigidbody.AddExplosionForce(force, centre, radius);

            if (_coroutine != null)
                StopCoroutine(DestroySegment());

            _coroutine = StartCoroutine(DestroySegment());
        }
    }

    private IEnumerator DestroySegment()
    {
        _renderer.material.DOFade(0, _delay);

        yield return _wait;

        gameObject.SetActive(false);
    }
}
