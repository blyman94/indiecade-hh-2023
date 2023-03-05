using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeGameManager : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;
    public static float managerSpeed = 1f;

    [SerializeField]
    float spawnrate = 1f;
    public static float managerSpawnrate = 1f;

    [SerializeField]
    float spawnerSecondsDelay = 1f;
    public static float managerSpawnerDelay = 1f;

    public static bool isGameOver = false;

    public static float minX;

    
    
    // Start is called before the first frame update
    void Start()
    {
        managerSpeed = speed;
        managerSpawnrate = 1/spawnrate;
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

    }

    public void LevelLost()
    {

    }
}
