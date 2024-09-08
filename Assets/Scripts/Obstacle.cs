using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public ColorSwitcher lastColorSwitcher;

    private GameManager manager;
    private Color[] initialColors;
    private Color[] obstacleColors;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();

        ShuffleColorsArray();

        AssignColorsToChildren();
    }

    public void SetInitialPlayerColor(PlayerController _player)
    {
        // Set the initial color of player as the first value in the colors array
        // To avoid that player's color does not match any color at the first obstacle 
        if (_player != null) _player.ChangePlayerColor(obstacleColors);
    }

    private void ShuffleColorsArray()
    {
        // Define colors array by the availableColors array from Game Manager
        initialColors = manager.availableColors;

        // Fisher-Yates shuffle algorithm to shuffle the availableColors array of Game Manager
        // In order that the parts of obstacle are in different colors in every start
        for (int i = initialColors.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Color _temp = initialColors[i];
            initialColors[i] = initialColors[j];
            initialColors[j] = _temp;
        }
    }

    private void AssignColorsToChildren()
    {
        // Count the numbers of parts this obstacle has
        int _childrenCount = transform.childCount;

        // Create color array to store all the color values of parts of this obstacle
        obstacleColors = new Color[_childrenCount];

        for (int i = 0; i < _childrenCount; i++)
        {
            // if the children count is smaller or equal to the length of colors array 
            if (initialColors[i] != null)
            {
                // Change the children's renderer colors by the colors array
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = initialColors[i];
            }
            else
            {
                // Change any one color in the array since all the colors are shown
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = initialColors[Random.Range(0, initialColors.Length)];
            }

            // Put the color value into the local colors array
            obstacleColors[i] = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
        }

        // Set the colors of this obstacle available at the below color switcher
        lastColorSwitcher.SetColors(obstacleColors);
    }
}