using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float _bounceForce = 50f;
    [SerializeField] private float _bounceRadius = 50f;

    public void Break()
    {
        PlatformSegment[] platformSegments = GetComponentsInChildren<PlatformSegment>();

        foreach (var segment in platformSegments)
        {
            segment.Bounce(_bounceForce, transform.position, _bounceRadius);
            segment.gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
