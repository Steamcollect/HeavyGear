using TMPro;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TMP_Text _coinText;

    [Header("References")]
    [Space(10)]
    [SerializeField] private RSO_Coins _rsoCoin;
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void Awake()
    {
        UpdateCoinUI();
    }

    private void OnEnable()
    {
        _rsoCoin.OnChanged += UpdateCoinUI;
    }

    private void OnDisable()
    {
        _rsoCoin.OnChanged -= UpdateCoinUI;
    }

    private void UpdateCoinUI()
    {
        _coinText.text = _rsoCoin.Value.ToString();
    }
}