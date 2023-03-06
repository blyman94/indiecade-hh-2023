using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Vector2Variable followPosition;
    public float followSpeed = 5f;
    public float stopDistance = 0.1f;
    [SerializeField] private Rigidbody2D rb2d;
    public bool Cinematic = false;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public bool literallyAnything = false;

    private void Start()
    {
        rb2d.velocity = Vector2.zero;
        
    }

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

        if (rb2d.velocity.x >= 0)
        {
            spriteRenderer.flipX = true;
        }
        if (rb2d.velocity.x < 0)
        {
            spriteRenderer.flipX = false;
        }

        if (literallyAnything)
        {
            spriteRenderer.flipX = false;
        }

        if (rb2d.velocity.magnitude > 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    public void MoveLeft()
    {
        Debug.Log("Called");
        rb2d.velocity = Vector2.left * followSpeed;
    }

    public void Stop()
    {
        rb2d.velocity = Vector2.zero;
    }
}
