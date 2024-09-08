using UnityEditor;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool isFirstObstacle;
    public ColorSwitcher[] lastColorSwitchers;

    private GameManager manager;
    private Color[] initialColors;
    private Color[] obstacleColors;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();

        ShuffleColorsArray();

        AssignColorsToChildren();

        // Manage the initial player color if this is the first obstacle
        if (isFirstObstacle) 
            SetInitialPlayerColor(FindObjectOfType<PlayerController>());
    }

    private void SetInitialPlayerColor(PlayerController _player)
    {
        // Set the initial color of player from the obstacle colors array
        // To avoid that player's color does not match any color at the first obstacle 
        if (_player != null) 
            _player.ChangePlayerColor(obstacleColors);
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
        
        SetColorSwitcher(lastColorSwitchers);
    }

    private void SetColorSwitcher(ColorSwitcher[] _switchers)
    {
        if (_switchers.Length > 0 && obstacleColors.Length > 0)
        {
            // Set the colors of this obstacle available at the below color switchers
            foreach (ColorSwitcher _switcher in _switchers)
                _switcher.SetColors(obstacleColors);
        }
    }
}

// Custom editor class for Obstacle script
/*[CustomEditor(typeof(Obstacle))]
public class Obstacle_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        var _script = (Obstacle)target;
        // Display a toggle field for checking if this is the first obstacle
        _script.isFirstObstacle = EditorGUILayout.Toggle(label: "isFirstObstacle", _script.isFirstObstacle);

        if (_script.isFirstObstacle == true)
            return;

        // Display an ObjectField for assigning the last Color Switcher if this is not the first obstacle 
        _script.lastColorSwitcher = EditorGUILayout.ObjectField(label: "Last Color Switcher", _script.lastColorSwitcher, typeof(ColorSwitcher), allowSceneObjects:true) as ColorSwitcher;
    }
}*/