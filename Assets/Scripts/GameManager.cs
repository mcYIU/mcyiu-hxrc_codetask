using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Colors Selection")]
    public Color[] availableColors;

    [Header("UI")]
    public TextMeshProUGUI text_NumCollectedStars;
    public GameObject winPage;
    public GameObject losePage;

    private SoundManager soundManager;
    private static int numCollectedStars = 0;
    public static int NumCollectedStars
    {
        get { return numCollectedStars; }
        set
        {
            if (numCollectedStars != value)
            {
                numCollectedStars = value;
                // Load every time the static num changes
                LoadNumCollectedStars();
            }
        }
    }

    private void Start()
    {
        soundManager = FindAnyObjectByType<SoundManager>();

        // Reset variables as Start
        NumCollectedStars = 0;
        winPage.SetActive(false);
        losePage.SetActive(false);

        // Enable the player ball's movement
        PlayerController.canMove = true;
    }

    private static void LoadNumCollectedStars()
    {
        // Update the TextMeshPro component with the new value of NumCollectedStars
        FindObjectOfType<GameManager>().text_NumCollectedStars.text = numCollectedStars.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnEndPageEnable(string _hitTag)
    {
        // if the ball hits the finish zone
        if (_hitTag == "Finish")
        {
            // Trigger win sound and win page
            soundManager.PlayCollectSound();
            winPage.SetActive(true);
        }
        else
        {
            // Trigger lose sound and lose page
            soundManager.PlayDeadSound();
            losePage.SetActive(true);
        }
    }
}
