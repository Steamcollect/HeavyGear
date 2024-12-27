using UnityEngine;

[CreateAssetMenu(fileName = "SSO_CardVisual", menuName = "ScriptableObject/SSO_CardVisual")]
public class SSO_CardVisual : ScriptableObject
{
    [Header("Main")]
    public Color firstColor;
    public Color secondColor;
}