using UnityEngine;
public class UIManager : MonoBehaviour
{
    [Header("Settings")]


    [Header("References")]
    [SerializeField] private RSO_Coins _rsoCoin;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    private void OnEnable()
    {
        _rsoCoin.OnChanged += UpdateCoinUI;
    }

    private void UpdateCoinUI()
    {

    }
}