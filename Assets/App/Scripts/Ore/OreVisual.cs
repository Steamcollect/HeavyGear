using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEngine;

public class OreVisual : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer visualSprite;
    [SerializeField] private TextMeshProUGUI valueText;

    public void UpdateVisual(Sprite visual)
    {
        if (visualSprite != null)
        {
            visualSprite.sprite = visual;
        }
    }
}
