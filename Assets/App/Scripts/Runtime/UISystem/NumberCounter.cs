using TMPro;
using UnityEngine;
public class NumberCounter : MonoBehaviour
{
    
    [Header("Settings")]
    [SerializeField] private int countFPS = 60;
    [SerializeField] private float duration = 1f;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI text;
}