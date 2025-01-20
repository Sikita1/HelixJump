using System;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public event Action GameOver;
    public event Action GameWin;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlatformSegment platformSegment))
            other.GetComponentInParent<Platform>().Break();
    }

    private void OnCollisionEnter(Collision collision)
    {
        FinishPlatform finishPlatform = collision.gameObject.GetComponentInParent<FinishPlatform>();

        if (finishPlatform != null)
            GameWin?.Invoke();
        //    finishPlatform.GameWinner();

        //if (collision.gameObject.TryGetComponent(out FinishPlatform finishPlatform))

        if (collision.gameObject.TryGetComponent(out DangerSegment dangerSegment))
            GameOver?.Invoke();
    }
}
