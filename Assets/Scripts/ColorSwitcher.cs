using UnityEngine;

public class ColorSwitcher : MonoBehaviour
{
    private Color[] selectedColors;

    public Color[] SwitchColors { get { return selectedColors; } }

    public void SetColors(Color[] _colors)
    {
        selectedColors = _colors;
    }
}
