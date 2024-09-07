using UnityEngine;

public class PlayerController : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ColorSwitcher"))
        {
            // Change player color to a new color
            ChangePlayerColor(other.GetComponent<ColorSwitcher>().changableColors);
            // Destroy color switcher
            Destroy(other.gameObject);
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
}
