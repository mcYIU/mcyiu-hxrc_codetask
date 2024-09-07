using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public Obstacle firstObstacle;
    public Color[] availableColors;

    private void Start()
    {
        if (player != null && firstObstacle != null) 
            firstObstacle.SetInitialPlayerColor(player);
    }
}
