using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ColorSwitcher colorSwitcher;

    private void Start()
    {
        if(colorSwitcher != null)
            // Initialize player color
            ChangePlayerColor(colorSwitcher.availableColors[Random.Range(0, colorSwitcher.availableColors.Length)]);
    }

    private void ChangePlayerColor(Color _newColor)
    {
        // Change player color
        GetComponent<SpriteRenderer>().color = _newColor;
        Debug.Log(_newColor.ToString());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ColorSwitcher"))
        {
            // Change ball color to a new color
            // Destroy color switcher
            Destroy(other.gameObject);
        }
    }
}
