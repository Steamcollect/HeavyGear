using UnityEngine;

[CreateAssetMenu(fileName = "SSO_MachineData", menuName = "ScriptableObject/SSO_MachineData")]
public class SSO_MachineData : ScriptableObject
{
    public float speed;
    public float cooldown;

    public float power;
    public int level;

    public int rarity;
}