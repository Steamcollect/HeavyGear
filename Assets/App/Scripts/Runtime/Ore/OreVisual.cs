using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OreVisual : MonoBehaviour
{
    [Title("References")]
    [SerializeField] private SpriteRenderer visualSprite;

    public void UpdateVisual(Sprite visual)
    {
        if (visualSprite != null)
        {
            visualSprite.sprite = visual;
        }
    }
}
