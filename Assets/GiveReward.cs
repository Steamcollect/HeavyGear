using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GiveReward : MonoBehaviour
{
    [SerializeField] private RSE_GiveReward rseGiveReward;
    [SerializeField] private RSE_GiveGem rseGiveGem;

    private void OnEnable()
    {
        rseGiveReward.action += GiveGem;
    }

    private void OnDisable()
    {
        rseGiveReward.action -= GiveGem;
    }

    private void GiveGem()
    {
        rseGiveGem.Call(100);
    }
}
