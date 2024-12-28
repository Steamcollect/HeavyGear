using BigFloatNumerics;
using TMPro;
using UnityEngine;
public class CheatCode : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private RSE_AddCoin rseAddCoin;
    
    [Header("Output")]
    [SerializeField] private RSE_LoadNewScene rseLoadNewScene;
    
    public void GiveMoney(TMP_InputField inputField)
    {
        rseAddCoin.Call(new BigNumber(inputField.text));
        inputField.text = "";
    }

    public void SwapToLevel(TMP_InputField inputField)
    {
        rseLoadNewScene.Call(inputField.text);
        inputField.text = "";
    }
    
}