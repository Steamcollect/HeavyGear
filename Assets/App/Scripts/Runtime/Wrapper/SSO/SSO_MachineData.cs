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
    public int level;

    public int rarity;

    public MachineData(float speed, float cooldown, float power, int level, int rarity)
    {
        this.speed = speed;
        this.cooldown = cooldown;
        this.power = power;
        this.level = level;
        this.rarity = rarity;
    }

    public MachineData Copy()
    {
        return new MachineData(speed, cooldown, power, level, rarity);
    }
}