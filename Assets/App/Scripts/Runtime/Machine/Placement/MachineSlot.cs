using UnityEngine;
public class MachineSlot : MonoBehaviour
{
    [SerializeField] MachineSlotSettings settings;
    [HideInInspector]public InteractiveMachineTemplate currentMachine;

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_AddNewMachine rseAddNewMachine;

    [Header("Output")]
    [SerializeField] RSE_OpenModificationSettings rseOpenModificationPanel;

    private void OnEnable()
    {
        settings.clickable.onClickUp += OnClick;
        settings.clickable.onLongClickDown += OnLongClick;
    }
    private void OnDisable()
    {
        settings.clickable.onClickUp -= OnClick;
        settings.clickable.onLongClickDown -= OnLongClick;
    }

    private void Awake()
    {
        settings.slot = this;
    }

    private void Start()
    {
        switch (settings.machineType)
        {
            case MachineType.Miner:
                if (settings.conveyorsExit.Length == 0)
                    Debug.LogError("Your slot is type of \"Miner\" but you haven't selected any exit conveyor!");
                break;

            case MachineType.Polisher:
                break;

            case MachineType.Seller:
                if (settings.conveyorsEnter.Length == 0)
                    Debug.LogError("Your slot is type of \"Seller\" but you haven't selected any enter conveyor!");
                break;
        }
    }

    public void UpdateCurrentMachine(InteractiveMachineTemplate machine)
    {
        currentMachine = machine;
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
        if(currentMachine != null)
        {
            rseOpenModificationPanel.Call(this);
        }
    }
}