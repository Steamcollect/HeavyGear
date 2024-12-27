using BigFloatNumerics;
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SSO_CoinLevel", menuName = "ScriptableObject/SSO_CoinLevel")]
public class SSO_CoinLevel : ScriptableObject
{
    [Title("Levels")]
    [InfoBox("Value must be written in the next format:  X 'e' Y ,X and Y correspond to a number and the e for the exponant." +
             "\n Click on the button to apply modifications.")]
    [SerializeField] string[] _rebirthLevelsEditor;
    [PropertySpace(5)]
    [SerializeField] string _nextFactoryLevelEditor;
    
    [Title("Scene")]
    public string nextFactorySceneName;
    [Space(10)]
    
    [FoldoutGroup("Debug")]
    [ReadOnly] public List<BigNumber> rebirthLevels = new();
    [FoldoutGroup("Debug")]
    [ReadOnly] public BigNumber nextFactoryLevel;

    private bool CheckValueCorrectFormat(string val)
    {
        if (val == "" || !val.Contains("e"))
        {
            Debug.LogWarning($"{val} is not a valid rebirth level value");
            return false;
        }

        return true;
    }
    
    [PropertySpace(10)]
    [Button("Apply Levels Modification")]
    private void ValidateModification()
    {
        rebirthLevels.Clear();
        foreach (var item in _rebirthLevelsEditor)
        {
            if (!CheckValueCorrectFormat(item)) continue;
            rebirthLevels.Add(new BigNumber(item));
        }
        
        if (!CheckValueCorrectFormat(_nextFactoryLevelEditor)) return;
        nextFactoryLevel = new BigNumber(_nextFactoryLevelEditor);
    }
}