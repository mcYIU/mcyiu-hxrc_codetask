using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioClip collectSound;
    public AudioClip colorSwitchSound;
    public AudioClip deadSound;
    public AudioSource AS;

    public void PlayClickSound()
    {
        if (AS != null && clickSound != null)
            AS.PlayOneShot(clickSound);
    }

    public void PlayCollectSound()
    {
        if (AS != null && collectSound != null)
            AS.PlayOneShot(collectSound);
    }

    public void PlayColorSwitchSound()
    {
        if (AS != null && colorSwitchSound != null)
            AS.PlayOneShot(colorSwitchSound);
    }

    public void PlayDeadSound()
    {
        if (AS != null && deadSound != null)
            AS.PlayOneShot(deadSound);
    }
}
