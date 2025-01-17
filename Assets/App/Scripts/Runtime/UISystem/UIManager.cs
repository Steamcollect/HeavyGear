using TMPro;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _gemText;

    [Header("References")]
    [Space(10)]
    [SerializeField] private RSO_Coins _rsoCoin;
    [SerializeField] private RSE_GiveGem _rseGem;

    private int gem;

    private void Awake()
    {
        UpdateCoinUI();
    }

    private void OnEnable()
    {
        _rsoCoin.OnChanged += UpdateCoinUI;
        _rseGem.action += UpdateGemUI;
    }

    private void OnDisable()
    {
        _rsoCoin.OnChanged -= UpdateCoinUI;
        _rseGem.action -= UpdateGemUI;
    }

    private void UpdateCoinUI()
    {
        Debug.LogWarning(_rsoCoin.Value.ToString());
        _coinText.text = _rsoCoin.Value.ToString();
    }

    private void UpdateGemUI(int quantity)
    {
        gem += quantity;
        _gemText.text = gem.ToString();
    }
}