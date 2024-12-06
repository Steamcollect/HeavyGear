using System.Collections;
using UnityEngine;
public abstract class InteractiveMachineTemplate : MonoBehaviour
{
    [Header("References")]
    public SSO_MachineData data;

    Clickable clickable;

    [HideInInspector] public MachineState currentState;

    public void SetupParentRequirement(Clickable newClickable)
    {
        clickable = newClickable;
        clickable.onClickDown += Interact;

        currentState = MachineState.Idle;

        OnObjEnable();
    }
    public abstract void OnObjEnable();
    private void OnDisable()
    {
        clickable.onClickDown -= Interact;

        OnObjDisable();
    }
    public abstract void OnObjDisable();

    void Interact()
    {
        print("nteract");
        if(currentState == MachineState.Idle && CanDoAction())
        {
            StartAction();
        }
    }

    #region IDLE
    void StartIdle()
    {
        currentState = MachineState.Idle;

        OnIdleStart();
    }
    public abstract void OnIdleStart();
    #endregion

    #region ACTION
    void StartAction()
    {
        currentState = MachineState.Action;

        OnActionStart();
    }
    public abstract void OnActionStart();

    public void EndAction()
    {
        StartCoroutine(ActionCooldown());
        currentState = MachineState.Cooldown;
    }

    public abstract bool CanDoAction();
    #endregion

    #region COOLDOWN
    void EndCooldown()
    {
        StartIdle();
    }
    public abstract void OnCooldownEnd();

    IEnumerator ActionCooldown()
    {
        yield return new WaitForSeconds(data.cooldown);
        EndCooldown();
    }
    #endregion

    public abstract void SetupChildRequirement(MachineSlotSettings settings);
}