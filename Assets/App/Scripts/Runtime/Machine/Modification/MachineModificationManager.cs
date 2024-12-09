using UnityEngine;
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

    [Header("Output")]
    [SerializeField] RSE_OpenMachineModificationPanel rseOpenModificationPanel;

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
        rseOpenModificationPanel.Call();
    }

    void DestroyMachine()
    {
        print("fsfss");
        Destroy(currentSlot.currentMachine.gameObject);
        currentSlot.UpdateCurrentMachine(null);
    }
}