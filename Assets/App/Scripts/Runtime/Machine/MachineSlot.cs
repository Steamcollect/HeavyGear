using UnityEngine;
public class MachineSlot : MonoBehaviour
{
    [SerializeField] MachineSlotSettings settings;
    InteractiveMachineTemplate currentMachine;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_AddNewMachine rseAddNewMachine;

    //[Header("Output")]

    private void OnEnable()
    {
        settings.clickable.onClickDown += OnClick;
        settings.clickable.onLongClickDown += OnLongClick;
    }
    private void OnDisable()
    {
        settings.clickable.onClickDown -= OnClick;
        settings.clickable.onLongClickDown -= OnLongClick;
    }

    void OnClick()
    {
        if(currentMachine == null)
        {
            rseAddNewMachine.Call(settings);
        }
    }

    void OnLongClick()
    {

    }
}