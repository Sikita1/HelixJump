using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlatformSegment platformSegment))
            other.GetComponentInParent<Platform>().Break();
    }

    private void OnCollisionEnter(Collision collision)
    {
        FinishPlatform finishPlatform = collision.gameObject.GetComponentInParent<FinishPlatform>();
        //DangerSegment dangerSegment = collision.gameObject.GetComponent<DangerSegment>();

        if (finishPlatform != null)
            finishPlatform.GameWinner();

        if (collision.gameObject.TryGetComponent(out DangerSegment dangerSegment))
        {
            Debug.Log("умер");
            dangerSegment.GameOvering();
        }
    }
}
