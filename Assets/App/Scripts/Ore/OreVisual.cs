using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class OreVisual : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer visualSprite;

    public void UpdateVisual(Sprite visual)
    {
        if (visualSprite != null)
        {
            visualSprite.sprite = visual;
        }
    }
}
