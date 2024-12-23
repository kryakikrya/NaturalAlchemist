using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedClass : MonoBehaviour
{
    [SerializeField] internal SeedElement SeedelEment;
    [SerializeField] internal PotionClass PotionElement;
    [SerializeField] internal SpriteRenderer spriteRenderer;

    private void Update()
    {
        if (PotionElement != null)
        {
            spriteRenderer.sprite = PotionElement.Sprite;
            spriteRenderer.color = PotionElement.Color;
        }
        else if (SeedelEment != null)
        {
            spriteRenderer.sprite = SeedelEment.Sprite;
            spriteRenderer.color = SeedelEment.Color;
        }

    }
}
