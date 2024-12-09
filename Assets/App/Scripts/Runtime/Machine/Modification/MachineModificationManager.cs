using UnityEngine;
public class MachineModificationManager : MonoBehaviour
{
    //[Header("Settings")]

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    //[Header("Input")]
    //[Header("Output")]

    void RemoveMachine(MachineSlot slot)
    {
        Destroy(slot.currentMachine.gameObject);
        slot.UpdateCurrentMachine(null);
    }
}