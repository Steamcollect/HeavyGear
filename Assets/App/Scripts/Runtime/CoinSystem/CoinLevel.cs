using System;
using Sirenix.OdinInspector;
using UnityEngine;
public class CoinLevel : MonoBehaviour
{

    [Title("References")]
    [SerializeField] private SSO_CoinLevel ssoCoinLevel;
    [Space(10)]
    [SerializeField] private RSO_Coins rsoCoins;
    [SerializeField] private RSO_ContentSaved rsoContentSaved;
    
    [Title("Output")]
    [SerializeField] private RSE_LoadNewScene rseLoadNewScene;

    public void OnEnable() => rsoCoins.OnChanged += CheckLevelReached;
    public void OnDisable() => rsoCoins.OnChanged -= CheckLevelReached;

    private void CheckLevelReached()
    {
        if (rsoCoins.Value.CompareTo(0) == 0) return;
        print(ssoCoinLevel.rebirthLevels[rsoContentSaved.Value.currentCoinLevel].ToString());
        int valueCompare = rsoCoins.Value.CompareTo(ssoCoinLevel.rebirthLevels[rsoContentSaved.Value.currentCoinLevel]);
        if (valueCompare is 1 or 0)
        {
            Debug.Log("Level reached");
        }
        // if (coins >= coinLevel.nextFactoryLevel)
        // {
        //     //Debug.Log("Next factory Unlock");
        //
        //     // Changer de scene si le joueur le souhaite
        //     //rseLoadNewScene.Call(coinLevel.nextFactorySceneName);
        // }
        //
        // for (int i = 0; i < coinLevel.rebirthLevels.Count; i++)
        // {
        //     if (coins >= coinLevel.rebirthLevels[i])
        //     {
        //         //Debug.Log("Rebirth " + i + ", at " + coinLevel.rebirthLevels[i] + "possible");
        //
        //         // Rebirth, charger la scene actuelle si le joueur le souhaite
        //         //rseLoadNewScene.Call( <Faut trouver le nom de la scene actuelle> );
        //     }
        // }
    }
}