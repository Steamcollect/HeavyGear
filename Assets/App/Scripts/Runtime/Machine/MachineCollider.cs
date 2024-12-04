using System;
using System.Collections.Generic;
using UnityEngine;
public class MachineCollider : MonoBehaviour
{
    public List<Ore> oresInCollision = new List<Ore>();

    public event Action<Ore> onItemEnter;
    public event Action<Ore> onItemExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Ore ore))
        {
            oresInCollision.Add(ore);
            onItemEnter?.Invoke(ore);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ore ore))
        {
            oresInCollision.Remove(ore);
            onItemExit?.Invoke(ore);
        }
    }
}