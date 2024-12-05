using UnityEngine;

[System.Serializable]
public class MachineSlotSettings
{
    public ConveyorBelt[] conveyorsEnter;
    public ConveyorBelt[] conveyorsExit;

    public Clickable clickable;

    public MachineType machineType;
    [HideInInspector] public MachineSlot slot;
}