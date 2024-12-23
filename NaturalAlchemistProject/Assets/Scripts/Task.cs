using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Task
{
    [SerializeField] internal List<PotionClass> AcceptableElements = new();
    [SerializeField] internal float MaxTime = 180;
    [SerializeField] internal string followuptext = "";
}
