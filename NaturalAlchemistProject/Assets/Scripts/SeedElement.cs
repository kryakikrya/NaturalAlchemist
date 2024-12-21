using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeedElement", menuName = "Create Seed Element")]
public class SeedElement : ScriptableObject
{
    [SerializeField] internal List<SeedElement> ComponentElements = new();
    [SerializeField] internal Color Color;
    [SerializeField] internal Sprite Sprite;
    [SerializeField] internal string ElementName;
}