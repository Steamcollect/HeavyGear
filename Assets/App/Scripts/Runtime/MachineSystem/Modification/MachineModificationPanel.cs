using UnityEngine;
public class MachineModificationPanel : MonoBehaviour
{
    //[Header("Settings")]

    [Header("References")]
    [SerializeField] GameObject panel;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Output")]
    [SerializeField] RSE_DestroyMachine rseDestroyMachine;

    public void DestroyButton()
    {
        rseDestroyMachine.Call();
    }
}