using UnityEngine;

public class ColorSwitcher : MonoBehaviour
{
    public Color[] changableColors;

    public void SetColors(Color[] _colors)
    {
        changableColors = _colors;
    }
}
