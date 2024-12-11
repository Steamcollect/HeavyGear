using UnityEngine;

[CreateAssetMenu(fileName = "SSO_CardVisual", menuName = "ScriptableObject/SSO_CardVisual")]
public class SSO_CardVisual : ScriptableObject
{
    [Header("Main")]
    public Color backgroundColor;

    [Header("Border")]
    public Color borderColor;
    public Color borderGlowColor;
    public Color borderShadowColor;

    [Header("Slider")]
    public Color sliderBackgroundColor;
    public Color sliderFillColor;
}