using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float yThreshold;
    public float force;
    public Transform finishZone;
    public ParticleSystem deadEffect;
    public ParticleSystem collectEffect;

    public static bool canMove;

    private GameManager manager;
    private Camera mainCamera;
    private Rigidbody2D rb;

    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();

        canMove = true;
    }

    void Update()
    {
        if (canMove)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Handle player input
                // Add force to the ball in the Y axis
                rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            }

            // Check if the ball goes above yPos of camera, and the top edge of camera reaches final zone
            if (transform.position.y > mainCamera.transform.position.y && transform.position.y + mainCamera.orthographicSize < finishZone.position.y)
            {
                // Adjust camera position
                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y, mainCamera.transform.position.z);
            }
            // If the ball is below the camera position
            else if (transform.position.y < mainCamera.transform.position.y)
            {
                // Keep the camera at its current yPos
                mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
            }

            // Check if the ball falls below the camera boundary
            if (transform.position.y < mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).y)
            {
                DestroyPlayer(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        string _tag = other.tag;

        switch (_tag)
        {
            // if hitting a color switcher
            case "ColorSwitcher":
                // Change the ball color to a new color
                ChangePlayerColor(other.GetComponent<ColorSwitcher>().SwitchColors);
                // Destroy color switcher
                Destroy(other.gameObject);
                break;

            // if hitting an obstacle
            case "Obstacle":
                // Access the obstacle color
                other.TryGetComponent<SpriteRenderer>(out SpriteRenderer _renderer);
                // Compare the obstacle color to the ball color
                if (_renderer.color != GetComponent<SpriteRenderer>().color)
                    // Destroy the ball if the colors are not the same
                    DestroyPlayer(false);
                break;

            // if hitting a collectable
            case "Collect":
                // increment the number of collected stars
                GameManager.NumCollectedStars++;
                // Instantiate the effect at the transform of the hit collectable 
                Instantiate(collectEffect, other.transform.position, Quaternion.identity);
                // Destroy the collectable
                Destroy(other.gameObject);
                break;

            // if hitting the finish zone
            case "Finish":
                // Trigger winning UI
                manager.OnEndPageEnable(_tag);
                DestroyPlayer(true);
                break;

            case null:
                DestroyPlayer(false);
                break;
        }
    }

    public void ChangePlayerColor(Color[] _newColors)
    {
        // Randomly get a color from the colors array
        Color _color = _newColors[Random.Range(0, _newColors.Length)];

        // Compare the current ball color to the new color
        if (_color != GetComponent<SpriteRenderer>().color)
            // Change player color
            GetComponent<SpriteRenderer>().color = _color;
        else
            // Get a new ball color again
            ChangePlayerColor(_newColors);
    }

    private void DestroyPlayer(bool _doesPlayerWin)
    {
        // Instantiate the effect at the ball's transform
        Instantiate(deadEffect, transform.position, Quaternion.identity);
        // Destroy the ball
        Destroy(gameObject);

        canMove = false;

        // Call lose page if the ball is killed out of finish zone
        if (!_doesPlayerWin)
            manager.OnEndPageEnable("");
    }
}
