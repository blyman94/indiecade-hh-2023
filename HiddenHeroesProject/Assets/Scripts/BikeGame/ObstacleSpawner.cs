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
            obstacleToSpawn = Mathf.RoundToInt(Random.Range(0, obstacles.Length - 1));
            yield return new WaitForSeconds(BikeGameManager.managerSpawnrate * Random.Range(0.5f, 1.5f) * 4);
            spawnLocation = Random.Range(-spawnerRadius, spawnerRadius);
            Instantiate(obstacles[obstacleToSpawn], new Vector3(transform.position.x, transform.position.y + spawnLocation, 0), transform.rotation);
        }
    }
}
