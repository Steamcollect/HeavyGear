using BigFloatNumerics;
using Sirenix.OdinInspector;
using UnityEngine;
public class GameInitator : MonoBehaviour
{
    [Title("References")]
    [SerializeField] private RSO_Coins rsoCoins;

    private void Awake() => rsoCoins.Value = new BigNumber(0);

}