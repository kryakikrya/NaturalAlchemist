using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TasksHandler : MonoBehaviour
{
    [Header("Set-Up")]
    [SerializeField] private List<Task> possibleTasks = new();
    [SerializeField] private Transform slotTransform;
    [SerializeField] private Task currentTask;
    [SerializeField] private TMP_Text textLabel;
    [Header("Values")]
    [SerializeField] internal int MoneyLoseGain = 5;
    [SerializeField] internal int MaxQueueSize = 5;
    [SerializeField] internal int MinQueueSize = 1;
    [SerializeField] internal float BetweenTasksWaitTime = 5f;
    [SerializeField] internal float NewQueueWaitTime = 15f;
    internal int Money = 10;
    private Queue<Task> _tasks = new();
    float timer = 10f;

    private Task GetNewTask()
    {
        int random = UnityEngine.Random.Range(0, possibleTasks.Count);
        return possibleTasks[random];
    }
    private void GenerateTasksList()
    {
        int size = UnityEngine.Random.Range(MinQueueSize, MaxQueueSize+1);
        for (int i = 0; i < size; i++)
        {
            Task newtask = GetNewTask();
            if (newtask != null) _tasks.Enqueue(newtask);
        }
    }

    private void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else if (timer <= 0f)
        {
            if (currentTask != null)
            {
                currentTask = null;
                Money -= MoneyLoseGain;
                timer = BetweenTasksWaitTime;
            }
            else if (currentTask == null && _tasks.Count > 0)
            {
                currentTask = _tasks.Dequeue();
                if (currentTask.followuptext != null) 
                { 
                    textLabel.text = currentTask.followuptext;
                }
            }
            else if (currentTask == null && _tasks.Count <= 0)
            {
                GenerateTasksList();
            }
        } 
        if (slotTransform.childCount > 0 && slotTransform.GetChild(0).TryGetComponent<SeedClass>(out SeedClass sclas) == true)
        {
            if (sclas != null && currentTask.AcceptableElements.Contains(sclas.element)) 
            {
                currentTask = null;
                Money += MoneyLoseGain;
                timer = BetweenTasksWaitTime;
            }
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
