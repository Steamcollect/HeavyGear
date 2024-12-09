using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class CallerRSEEvent : MonoBehaviour
{
    [Title("Output")]
    [SerializeField] private BT.ScriptablesObject.RuntimeScriptableEvent rse;

    public void CallRSE() => rse.Call();
}