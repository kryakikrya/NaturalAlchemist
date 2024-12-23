using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Create Potion")]
public class PotionClass : ScriptableObject
{
    [SerializeField] internal List<SeedElement> ComponentElement;
    [SerializeField] internal Color Color;
    [SerializeField] internal Sprite Sprite;
    [SerializeField] internal string ElementName;
}
