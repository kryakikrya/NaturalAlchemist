using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedClass : MonoBehaviour
{
    [SerializeField] internal SeedElement element;
    [SerializeField] internal SpriteRenderer spriteRenderer;

    private void Update()
    {
        if (element != null)
        {
            spriteRenderer.sprite = element.Sprite;
            spriteRenderer.color = element.Color;
        }
    }
}
