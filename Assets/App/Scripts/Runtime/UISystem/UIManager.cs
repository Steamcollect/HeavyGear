using System.Threading.Tasks;
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

    private void Start()
    {
        StartCoroutine(Utils.Delay(0.15f, ()=>
        {
            UpdateCoinUI();
            UpdateGemUI(0);
        }));
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
        if (_rsoCoin.Value == null) return;
        _coinText.text = _rsoCoin.Value.ToString();
    }

    private void UpdateGemUI(int quantity)
    {
        gem += quantity;
        _gemText.text = gem.ToString();
    }
}