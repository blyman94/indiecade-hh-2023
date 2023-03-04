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
    
    // Start is called before the first frame update
    void Start()
    {
        managerSpeed = speed;
        managerSpawnrate = spawnrate;
        managerSpawnerDelay = spawnerSecondsDelay;
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
