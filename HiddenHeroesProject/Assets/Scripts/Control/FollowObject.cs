using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Vector2Variable followPosition;
    public float followSpeed = 5f;
    public float stopDistance = 0.1f;
    [SerializeField] private Rigidbody2D rb2d;
    public bool Cinematic = false;

    private void FixedUpdate()
    {
        if (!Cinematic)
        {
            Vector2 direction = (followPosition.Value - (Vector2)transform.position).normalized;
            float distance = Vector2.Distance(transform.position, followPosition.Value);

            if (distance > stopDistance)
            {
                rb2d.velocity = direction * followSpeed;
            }
            else
            {
                rb2d.velocity = Vector2.zero;
            }
        }
    }
}