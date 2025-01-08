using UnityEngine;

[CreateAssetMenu(fileName = "SSO_MachineData", menuName = "ScriptableObject/SSO_MachineData")]
public class SSO_MachineData : ScriptableObject
{
    public MachineData data;
}

[System.Serializable]
public class MachineData
{
    public float speed;
    public float cooldown;

    public float power;
    public int duration;

    public MachineData(float speed, float cooldown, float power, int rarity)
    {
        this.speed = speed;
        this.cooldown = cooldown;
        this.power = power;
        this.duration = rarity;
    }

    public MachineData Copy()
    {
        return new MachineData(speed, cooldown, power, duration);
    }
}