using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class ReceiverRSEEvent : MonoBehaviour
{
    [Title("Input")]
    [SerializeField] private BT.ScriptablesObject.RuntimeScriptableEvent rse;

    [Title("Output")] 
    [SerializeField] private UnityEvent onRSECalled;

    private void OnEnable() => rse.action += onRSECalled.Invoke;
    private void OnDisable() => rse.action -= onRSECalled.Invoke;
}