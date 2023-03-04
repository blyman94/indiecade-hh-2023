using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeDriving : MonoBehaviour
{
    [SerializeField]
    float maxVerticalSpeed = 5f;
    [SerializeField]
    float verticalSpeed = 50f;
    [SerializeField]
    float verticalDampening = 1f;
    private float verticalInput;
    public static float completion = 30f;

    [SerializeField]
    Rigidbody rb;

    private void Awake()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    private void FixedUpdate()
    {
        verticalInput = Input.GetAxisRaw("Vertical");

        float move = verticalInput * verticalSpeed * Time.fixedDeltaTime;
        Vector3 moveDirection = new Vector3(0f, move, 0f);
        //rb.MovePosition(rb.position + moveDirection);
        rb.AddForce(moveDirection, ForceMode.Force);
        if (rb.velocity.magnitude > maxVerticalSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxVerticalSpeed;
        }

        if (Mathf.Approximately(verticalInput, 0f))
        {
            float currentVelocity = rb.velocity.y;
            float newVelocity = Mathf.Lerp(currentVelocity, 0f, Time.fixedDeltaTime * verticalDampening);
            rb.velocity = new Vector3(rb.velocity.x, newVelocity, rb.velocity.z);
        }
    }

    public void slowDown(float slowAmount)
    {
        completion -= slowAmount;
    }
}
