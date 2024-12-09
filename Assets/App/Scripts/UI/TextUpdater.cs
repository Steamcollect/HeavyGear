using BT.ScriptablesObject;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
public abstract class TextUpdater<T> : MonoBehaviour
{
    [Title("References")]
    [SerializeField] private TMP_Text tmptext;
    [Title("Input")] 
    [SerializeField] protected RuntimeScriptableObject<T> rso;
    
    private void OnEnable()
    {
        rso.OnChanged += UpdateText;
        UpdateText();
    }

    private void OnDisable() => rso.OnChanged -= UpdateText;

    protected abstract string GetValueTextShow();

    private void UpdateText() => tmptext.text = GetValueTextShow();
}