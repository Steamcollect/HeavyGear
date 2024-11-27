using System.Collections;
using UnityEngine;
public abstract class MachineTemplate : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SSO_MachineData data;

    [Space(10)]
    [SerializeField] Clickable clickable;

    [HideInInspector] public MachineState currentState;

    private void OnEnable()
    {
        clickable.onClickDown += OnClickDown;

        OnObjEnable();
    }
    public abstract void OnObjEnable();
    private void OnDisable()
    {
        clickable.onClickDown -= OnClickDown;

        OnObjDisable();
    }
    public abstract void OnObjDisable();

    void OnClickDown()
    {
        if(currentState == MachineState.Idle)
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
}