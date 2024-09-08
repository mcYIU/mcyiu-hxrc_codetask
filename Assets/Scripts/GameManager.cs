using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public Color[] availableColors;

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

    public static void LoadNumCollectedStars()
    {
        Debug.Log(_numCollectedStars);
    }

    public void DisplayWinStatement()
    {

    }
}
