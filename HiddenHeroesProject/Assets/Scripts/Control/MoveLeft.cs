using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = Vector2.left * moveSpeed;
        animator.SetBool("IsWalking", true);
    }
    public void Stop()
    {
        rb.velocity = Vector2.zero;
        animator.SetBool("IsWalking", false);
    }
}
