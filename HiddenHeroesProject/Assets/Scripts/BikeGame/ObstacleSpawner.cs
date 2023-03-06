using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] obstacles;
    private int obstacleToSpawn = 0;
    private float spawnLocation = 0;
    private float spawnerRadius;

    // Start is called before the first frame update
    void Start()
    {
        spawnerRadius = transform.localScale.y / 2;
        StartCoroutine(SpawnObjects());
    }

    // Update is called once per frame

    void Update()
    {
        
    }

    IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(BikeGameManager.managerSpawnerDelay);
        Debug.Log("entered spawnobjects");
        while (!BikeGameManager.isGameOver) 
        {
            Debug.Log("spawned obstacle");
            /*
                You originally had:
                     obstacleToSpawn = Mathf.RoundToInt(Random.Range(0, obstacles.Length - 1));

                Couple of things: The Random.Range(int, int) function will always
                return an integer. No need to round it! In fact, doing so hid a
                bug...

                Random.Range(int, int)'s second argument is exclusive. So
                Random.Range(0,1) can only return 0. Random.Range(0,2) can
                return 0 and 1. Therefore, you don't need to subtract 1 from the
                obstacle length to access all in the array.
            */
            obstacleToSpawn = Random.Range(0, obstacles.Length);
            yield return new WaitForSeconds(BikeGameManager.managerSpawnrate * Random.Range(0.5f, 1.5f) * 4);
            spawnLocation = Random.Range(-spawnerRadius, spawnerRadius);
            Instantiate(obstacles[obstacleToSpawn], new Vector3(transform.position.x, transform.position.y + spawnLocation, 0), transform.rotation);
        }
    }
}
