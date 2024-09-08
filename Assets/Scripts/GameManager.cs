using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public Color[] availableColors;
    public TextMeshProUGUI text_NumCollectedStars;

    private static int _numCollectedStars = 0;
    public static int NumCollectedStars
    {
        get { return _numCollectedStars; }
        set
        {
            if (_numCollectedStars != value)
            {
                _numCollectedStars = value;
                // Load every time the static num changes
                LoadNumCollectedStars();
            }
        }
    }

    private static void LoadNumCollectedStars()
    {
        // Update the TextMeshPro component with the new value of NumCollectedStars
        FindObjectOfType<GameManager>().text_NumCollectedStars.text = _numCollectedStars.ToString();
    }

    public void DisplayWinStatement()
    {

    }
}
