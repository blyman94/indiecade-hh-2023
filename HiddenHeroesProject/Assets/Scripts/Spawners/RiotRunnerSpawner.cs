using UnityEngine;

public class RiotRunnerSpawner : MonoBehaviour
{
    public ObstaclePool obstaclePool;
    public Transform minYPosition;
    public Transform maxYPosition;
    public float spawnRate = 1f;

    private void Start()
    {
        InvokeRepeating("SpawnPrefab", 0f, 1f / spawnRate);
    }

    public void Stop()
    {
        CancelInvoke();
    }

    private void SpawnPrefab()
    {
        // Calculate a random y position between the minYPosition and maxYPosition
        float randomY = Random.Range(minYPosition.position.y, maxYPosition.position.y);

        // Spawn the prefab at the calculated y position
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, transform.position.z);
        GameObject runnerObject = obstaclePool.Pool.Get();
        runnerObject.transform.position = spawnPosition;
    }
}
