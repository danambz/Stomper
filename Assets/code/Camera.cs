using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    public Vector2 offset;
    public float smoothTime = 0.2f;
    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        if (target == null) return;
        Vector3 desired = new Vector3(target.position.x + offset.x,
                                      transform.position.y,
                                      transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, desired, ref velocity, smoothTime);
    }
}
