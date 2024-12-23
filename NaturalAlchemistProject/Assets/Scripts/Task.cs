using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    [SerializeField] internal List<SeedElement> AcceptableElements = new();
    [SerializeField] internal float MaxTime = 180;
    [SerializeField] internal string followuptext = "";
}
