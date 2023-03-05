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
    [SerializeField]
    Transform goal;
    [SerializeField]
    Transform fail;
    private float verticalInput;
    public static float completion = 30f;

    private float idleTime = 0;

    [SerializeField]
    Rigidbody2D rb;

    IEnumerator myCoroutine;

    private void Awake()
    {
        //rb.constraints = RigidbodyConstraints.FreezePositionZ;
        
    }
    private void Start()
    {
        myCoroutine = GainGround();
        StartCoroutine(myCoroutine);
    }

    private void FixedUpdate()
    {
        verticalInput = Input.GetAxisRaw("Vertical");

        float move = verticalInput * verticalSpeed * Time.fixedDeltaTime;
        Vector2 moveDirection = new Vector3(0f, move);
        //rb.MovePosition(rb.position + moveDirection);
        rb.AddForce(moveDirection);
        if (rb.velocity.magnitude > maxVerticalSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxVerticalSpeed;
        }

        if (Mathf.Approximately(verticalInput, 0f))
        {
            float currentVelocity = rb.velocity.y;
            float newVelocity = Mathf.Lerp(currentVelocity, 0f, Time.fixedDeltaTime * verticalDampening);
            rb.velocity = new Vector2(rb.velocity.x, newVelocity);
        }

        float dist = goal.position.x - fail.position.x;
        float distGain = completion / 100 * dist;
        float newPos = distGain + fail.position.x;
        Vector2 bikePos = new Vector2(newPos, transform.position.y);
        if (!Mathf.Approximately(transform.position.x, newPos))
        {
            transform.position = Vector2.Lerp(transform.position, bikePos, Time.deltaTime);
        }

        if (rb.velocity.x < 0.5f)
        {
            idleTime += Time.deltaTime;
        } else
        {
            idleTime = 0;
        }
    }

    public void slowDown(float slowAmount)
    {
        Debug.Log("slowdown!");
        idleTime = 0;
        completion -= slowAmount;
    }

    IEnumerator GainGround ()
    {
        while (!BikeGameManager.isGameOver)
        {
            if (idleTime > 1f)
            {
                yield return new WaitForSeconds(0.5f);
                completion += 1;
            }

            yield return null;
        }
    }
}
