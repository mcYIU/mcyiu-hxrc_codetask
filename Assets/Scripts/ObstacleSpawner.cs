using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval;
    public float obstacleMoveSpeed;

    private float spawnTimer = 0.0f;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        // Check if it's time to spawn a new obstacle
        if (spawnTimer >= spawnInterval)
        {
            SpawnObstacle();
            // Reset the spawn timer
            spawnTimer = 0.0f;
        }
    }

    private void SpawnObstacle()
    {
        // Instantiate a new obstacle at the spawner's position
        GameObject _newPrefab = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
        // Start moving the obstacle
        StartCoroutine(MoveObstacle(_newPrefab));
    }

    private IEnumerator MoveObstacle(GameObject _obj)
    {
        // Move the obstacle leftward until it reaches the left edge of the screen
        while (_obj != null && _obj.transform.position.x > Camera.main.ScreenToWorldPoint(Vector3.zero).x)
        {
            // Move the obstacle left
            _obj.transform.position += Vector3.left * obstacleMoveSpeed * Time.deltaTime;
            yield return null;       
        }

        // If the obstacle still exists, destroy it
        if (_obj != null) Destroy(_obj);    
    }
}