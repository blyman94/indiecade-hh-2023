using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeGameManager : MonoBehaviour
{
    /*
        Aha! You fell into my trap. With the setup described in the BikeDriving
        script, you don't really need the game manager to handle your win and 
        loss conditions anymore. We can assign GameEventListeners in the 
        inspector to handle what happens with the PlayerWin and PlayerLoss 
        events.
    */
    [SerializeField]
    float speed = 1f;
    public static float managerSpeed = 1f;

    [SerializeField]
    float spawnrate = 1f;
    public static float managerSpawnrate = 1f;

    [SerializeField]
    float spawnerSecondsDelay = 1f;
    public static float managerSpawnerDelay = 1f;

    /*
        Taksin! You made this static! That means it's a property of the class
        and not the instance of the class. That means it carries its value over
        the life of the program, and therefore keeps state when you don't want
        it to.

        In this case, the BikeDriving.cs class relies on this parameter to be 
        false to start the level. But, if the player loses on this level,
        isGameOver becomes true. Then the level reloads... and isGameOver
        remains true XD

        This is a hotfix, but we are going to set isGameOver to false in the
        awake function. It will be called before BikeDriving.cs start function,
        so we should get our game loop back. This issue with this approach is
        that it creates a race condition - meaning two scripts are racing to 
        give eachother information. Another sign of a tightly coupled set 
        of classes!

        hmm... that didn't work. You must be saving state somewhere else. I
        may need your help to find it.
    */

    public static bool isGameOver = false;

    public static float minX;

    void Awake()
    {
        isGameOver = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        managerSpeed = speed;
        managerSpawnrate = 1 / spawnrate;
        managerSpawnerDelay = spawnerSecondsDelay;

        Camera cam = Camera.main;

        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        minX = cam.transform.position.x - camWidth / 2f - 10;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LevelWin()
    {
        Debug.Log("You Win!");
        //insert winning implementation here
    }

    public void LevelLost()
    {
        Debug.Log("You Lost!");
        //insert loosing implementation here
    }
}
