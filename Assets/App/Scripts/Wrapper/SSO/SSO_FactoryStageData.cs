using BigFloatNumerics;
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SSO_FactoryStageData", menuName = "ScriptableObject/SSO_FactoryStageData")]
public class SSO_FactoryStageData : ScriptableObject
{
    [Title("Levels")]
    [InfoBox("Value must be written in the next format:  X 'e' Y ,X and Y correspond to a number and the e for the exponant." +
             "\n Click on the button to apply modifications.")]
    [SerializeField] string[] _rebirthStageEditor;
    [PropertySpace(5)]
    [SerializeField] string _nextFactoryStageEditor;
    
    [Title("Scene")]
    public string nextFactorySceneName;
    [Space(10)]
    
    [FoldoutGroup("Debug")]
    [ReadOnly] public List<BigNumber> rebirthStage = new();
    [FoldoutGroup("Debug")]
    [ReadOnly] public BigNumber nextFactoryStage;

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
    [Button("Apply Stages Modification")]
    private void ValidateModification()
    {
        rebirthStage.Clear();
        foreach (var item in _rebirthStageEditor)
        {
            if (!CheckValueCorrectFormat(item)) continue;
            rebirthStage.Add(new BigNumber(item));
        }
        
        if (!CheckValueCorrectFormat(_nextFactoryStageEditor)) return;
        nextFactoryStage = new BigNumber(_nextFactoryStageEditor);
    }
}