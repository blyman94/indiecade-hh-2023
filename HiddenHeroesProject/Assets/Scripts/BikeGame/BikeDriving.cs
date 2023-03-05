using System;
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
    [SerializeField]
    float iFrameSeconds = 1f;
    private bool isInvincible = false;
    private float verticalInput;
    public static float completion = 30f;

    private float idleTime = 0;
    private float bump = 1f; 

    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    GameObject gameMan;

    IEnumerator gainCoroutine;

    private void Awake()
    {
        //rb.constraints = RigidbodyConstraints.FreezePositionZ;
        
    }
    private void Start()
    {
        gainCoroutine = GainGround();
        StartCoroutine(gainCoroutine);
    }

    private void Update()
    {
        if (transform.position.x > goal.position.x)
        {
            StopCoroutine(gainCoroutine);
            gameMan.GetComponent<BikeGameManager>().LevelWin();
        }
        if (transform.position.x < fail.position.x)
        {
            StopCoroutine(gainCoroutine);
            gameMan.GetComponent<BikeGameManager>().LevelLost();
        }
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
            transform.position = Vector2.Lerp(transform.position, bikePos, Time.deltaTime * bump);
        }

        if (!Input.GetButton("Vertical"))
        {
            //Debug.Log("velocity " + rb.velocity.y);
            idleTime += Time.deltaTime;
        } else
        {
            idleTime = 0;
        }
    }

    public void slowDown(float slowAmount)
    {
        if (!isInvincible) 
        {
            StartCoroutine(BumpMovement());
            idleTime = 0;
            completion -= slowAmount; 
        }
    }

    IEnumerator GainGround ()
    {
        while (!BikeGameManager.isGameOver)
        {
            if (idleTime > 1f)
            {
                yield return new WaitForSeconds(0.5f);
                completion += 1 * Mathf.Log(idleTime/2 + 1);
            }

            yield return null;
        }
    }

    IEnumerator BumpMovement()
    {
        isInvincible = true;
        bump = 3f;
        yield return new WaitForSeconds(iFrameSeconds);
        bump = 1f;
        isInvincible = false;
    }
}
