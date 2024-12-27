using UnityEngine;
using UnityEngine.Events;
public class MachineModificationManager : MonoBehaviour
{
    //[Header("Settings")]

    //[Header("References")]
    MachineSlot currentSlot;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_OpenModificationSettings rseOpenModificationSettings;
    [SerializeField] RSE_DestroyMachine rseDestroyMachine;

    [SerializeField] private UnityEvent openModificationPanel;

    private void OnEnable()
    {
        rseOpenModificationSettings.action += OpenModificationSettings;
        rseDestroyMachine.action += DestroyMachine;
    }
    private void OnDisable()
    {
        rseOpenModificationSettings.action -= OpenModificationSettings;
        rseDestroyMachine.action -= DestroyMachine;
    }

    void OpenModificationSettings(MachineSlot slot)
    {
        currentSlot = slot;
        openModificationPanel.Invoke();
    }

    void DestroyMachine()
    {
        Destroy(currentSlot.currentMachine.gameObject);
        currentSlot.UpdateCurrentMachine(null);
    }
}