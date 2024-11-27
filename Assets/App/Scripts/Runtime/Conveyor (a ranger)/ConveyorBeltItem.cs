using UnityEngine;

[System.Serializable]
public class ConveyorBeltItem
{
    public Transform item;

    [HideInInspector] public float currentLerp = 1;
    [HideInInspector] public int startPoint = -1;

    [HideInInspector] public float pathDistance;

    public ConveyorBeltItem(Transform item)
    {
        this.item = item;
    }
}