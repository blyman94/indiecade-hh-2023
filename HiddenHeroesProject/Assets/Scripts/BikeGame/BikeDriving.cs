using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeDriving : MonoBehaviour
{
    [SerializeField] private GameEvent PlayerWinEvent;
    [SerializeField] private GameEvent PlayerLoseEvent;

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
    private bool isInvincible = true;
    private float verticalInput;

    /*
        Great work, Afraz!

        Taksin - Here's a breakdown:

        The following line used to read 

            public static float completion = 30f

        That means, that its a property of the class and not the instance of the
        class. When the program first starts running, the static initializations
        that you have here take place. They do not update when the scene is
        reloaded. Therefore, the program set completion to 30 the first time, 
        but as the player hits obstacles, completion decreases. When it was 
        static, the script "remembered" the completion, and never reset it. 
        Since completion is tied to the player's position, this caused the 
        bug when the scene was reloaded.

        The morale of the story? Try to avoid statics at all costs. There are 
        very specific use cases for them that you will notice when you remove
        them from your typical problem solving toolkit. I rarely use them 
        specifically for this reason - they make it hard to track where bugs are 
        coming from!

        Another hotfix here would be to have completion reset to 30 in the Awake
        function - that way you know it will be called in the first frame
        whenever a scene containing it is loaded.
    */
    public float completion = 30f;

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
        StartCoroutine(Setup());
    }

    /*
        Ah yes, this is a classic usecase for my GameEvent class. Here, you are
        checking every frame whether the loss condition has been met, and if it
        has, you are alerting the game manager. This couples together the game
        manager and the bike driving class, when their functionality should
        really be separate. Instead, you could add two fields to this class:

        [SerializeField] private GameEvent PlayerWinEvent;
        [SerializeField] private GameEvent PlayerLoseEvent;

        Replace 
            gameMan.GetComponent<BikeGameManager>().LevelWin();
        With 
            PlayerWinEvent.Raise();
        And Replace 
            gameMan.GetComponent<BikeGameManager>().LevelLost();
        With 
            PlayerLoseEvent.Raise();
        
        Check the game manager class for what happens next!
    */
    private void Update()
    {
        if (transform.position.x > goal.position.x && !BikeGameManager.isGameOver)
        {
            StopCoroutine(gainCoroutine);
            BikeGameManager.isGameOver = true;
            //gameMan.GetComponent<BikeGameManager>().LevelWin();
            PlayerWinEvent.Raise();
        }
        if (transform.position.x < fail.position.x && !BikeGameManager.isGameOver && !isInvincible)
        {
            StopCoroutine(gainCoroutine);
            BikeGameManager.isGameOver = true;
            //gameMan.GetComponent<BikeGameManager>().LevelLost();
            PlayerLoseEvent.Raise();
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

        if (!Input.GetButton("Vertical") && !isInvincible)
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

    IEnumerator Setup()
    {
        bump = 0.5f;
        isInvincible = true;
        yield return new WaitForSeconds(6f);
        bump = 1f;
        isInvincible = false;
    }
}
