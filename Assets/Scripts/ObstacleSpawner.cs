using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public ColorSwitcher[] lastColorSwitchers;
    public float spawnInterval;
    public float obstacleMoveSpeed;

    private float spawnTimer = 0.0f;

    private void Start()
    {
        // Find the position of right edge of the screen
        Vector3 screenRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 10)); 
        // Make this spawner always at the right edge
        transform.position = new Vector3(screenRightEdge.x, transform.position.y, transform.position.z);
    }

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

        // Set the colors of this obstacle available at the below color switchers
        _newPrefab.TryGetComponent<Obstacle>(out Obstacle _obstacle);
        _obstacle.lastColorSwitchers = lastColorSwitchers;

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