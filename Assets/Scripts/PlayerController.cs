using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float yThreshold;
    public float force;
    public ParticleSystem deadEffect;
    public ParticleSystem collectEffect;

    private Rigidbody2D rb;
    private bool canMove;

    void Start()
    {
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

            // Check if ball goes above the Y threshold
            if (transform.position.y > yThreshold)
            {
                // Camera follows the ball up
                Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
            }

            // Check if ball falls below the camera boundary
            if (transform.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y)
            {
                DestroyPlayer();
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
                // Change player color to a new color
                ChangePlayerColor(other.GetComponent<ColorSwitcher>().SwitchColors);
                // Destroy color switcher
                Destroy(other.gameObject);
                break;

            // if hitting an obstacle
            case "Obstacle":
                // Access the obstacle color
                other.TryGetComponent<SpriteRenderer>(out SpriteRenderer _renderer);
                // Compare the obstacle color to player color
                if (_renderer.color != GetComponent<SpriteRenderer>().color)
                    // Destroy player if the colors are not the same
                    DestroyPlayer();
                break;

            // if hitting a collectable
            case "Collect":
                GameManager.NumCollectedStars++;
                Instantiate(collectEffect, other.transform.position, Quaternion.identity);
                Destroy(other.gameObject);
                break;
        }
    }

    public void ChangePlayerColor(Color[] _newColors)
    {
        // Randomly get a color from the colors array
        Color _color = _newColors[Random.Range(0,_newColors.Length)];

        // Compare the current player color to the new color
        if (_color != GetComponent<SpriteRenderer>().color)
            // Change player color
            GetComponent<SpriteRenderer>().color = _color;
        else
            // Get a new player color again
            ChangePlayerColor(_newColors);
    }

    private void DestroyPlayer()
    {
        Instantiate(deadEffect, transform.position, Quaternion.identity);        
        Destroy(gameObject);

        canMove = false;
    }
}
