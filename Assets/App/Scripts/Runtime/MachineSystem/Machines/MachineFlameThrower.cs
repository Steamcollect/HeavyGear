using System.Collections;
using BigFloatNumerics;
using UnityEngine;
using UnityEngine.Events;
public class MachineFlameThrower : InteractiveMachineTemplate
{
    [Header("Internal Settings")]
    [SerializeField] CalculType calculType;
    [SerializeField] string value;

    [SerializeField] ParticleSystem flameParticle;

    BigNumber Value => new BigNumber(value);

    [Space(10)]
    [SerializeField] MachineCollider oreCollider;
    [SerializeField] Transform rotationPivot;

    bool isActive = false;

    [Header("Output")]
    [SerializeField] private UnityEvent onMachineAction;
    [SerializeField] private UnityEvent onMachineEndAction;

    public override void OnObjEnable()
    {
        oreCollider.onItemEnter += OnItemTouch;
    }

    public override void OnObjDisable()
    {
        oreCollider.onItemEnter -= OnItemTouch;
    }

    private void Update()
    {
        rotationPivot.Rotate(Vector3.up * statistics.speed * Time.deltaTime);
    }

    public override void OnIdleStart()
    {
        // Do nothing
    }

    #region Action
    public override void OnActionStart()
    {
        onMachineAction.Invoke();
        StartCoroutine(ActionDelay());
    }

    void OnItemTouch(Ore ore)
    {
        if (isActive)
        {
            switch (calculType)
            {
                case CalculType.Add:
                    ore.AddValue(Value);
                    break;

                case CalculType.Remove:
                    ore.RemoveValue(Value);
                    break;
                case CalculType.Multiply:
                    ore.MultiplyValue((float)Value);
                    break;
            }
        }
    }

    IEnumerator ActionDelay()
    {
        isActive = true;
        flameParticle.Play();
        yield return new WaitForSeconds(statistics.duration);
        isActive = false;
        flameParticle.Stop();

        onMachineEndAction.Invoke();
        EndAction();
    }
    #endregion

    public override void OnCooldownEnd()
    {
        // Do nothing
    }

    public override void SetupChildRequirement(MachineSlotSettings settings)
    {
        // Do nothing
    }

    public override bool CanDoAction()
    {
        return true;
    }

    public override float CooldownMultiplier()
    {
        return 1;
    }
}