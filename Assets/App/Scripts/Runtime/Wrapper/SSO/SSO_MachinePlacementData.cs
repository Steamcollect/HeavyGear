using UnityEngine;

[CreateAssetMenu(fileName = "SSO_MachinePlacementData", menuName = "ScriptableObject/SSO_MachinePlacementData")]
public class SSO_MachinePlacementData : ScriptableObject
{
    public string machineName;
    [TextArea] public string machineDescription;

    [Space(5)]
    public Sprite machineVisual;

    [Space(5)]
    public MachineType machineType;

    [Space(10)]
    public InteractiveMachineTemplate machinePrefab;
}