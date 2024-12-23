using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TasksHandler : MonoBehaviour
{
    [Header("Set-Up")]
    [SerializeField] private List<Task> possibleTasks;
    [SerializeField] private Transform slotTransform;
    [SerializeField] private Task tutorialtask; 
    [SerializeField] private TMP_Text textLabel;
    [Header("Values")]
    [SerializeField] internal int MoneyLoseGain = 5;
    [SerializeField] internal int MaxQueueSize = 5;
    [SerializeField] internal int MinQueueSize = 1;
    [SerializeField] internal float BetweenTasksWaitTime = 5f;
    [SerializeField] internal float NewQueueWaitTime = 15f;
    internal int Money = 10;
    private Queue<Task> _tasks = new();
    private Task currentTask;
    float timer = 10f;

    private void Start()
    {
        if (tutorialtask.MaxTime >0)
        {
            currentTask = tutorialtask;
            textLabel.text = tutorialtask.followuptext;
            timer = tutorialtask.MaxTime;
        }
    }
    private Task GetNewTask()
    {
        int random = UnityEngine.Random.Range(0, possibleTasks.Count);
        Task pickedclass = possibleTasks[random];
        Task randompotion = new Task();
        randompotion.MaxTime = pickedclass.MaxTime;
        randompotion.followuptext = pickedclass.followuptext;
        randompotion.AcceptableElements = new();
        foreach (PotionClass potion in pickedclass.AcceptableElements) 
        {
            randompotion.AcceptableElements.Add(potion);
        }
        return randompotion;
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
                textLabel.text = "Task failed,you will get fined.\n Wait for a new task.";
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
                timer = currentTask.MaxTime;
            }
            else if (currentTask == null && _tasks.Count <= 0)
            {
                GenerateTasksList();
            }
        } 
        if (slotTransform.childCount > 0 &&  slotTransform.GetChild(0).TryGetComponent<SeedClass>(out SeedClass sclas) == true)
        {
            if (sclas != null &&  sclas.PotionElement != null && currentTask.AcceptableElements.Contains(sclas.PotionElement)) 
            {
                if (currentTask.AcceptableElements.Count > 1)
                {
                    currentTask.AcceptableElements.Remove(sclas.PotionElement);
                } else
                {
                    currentTask = null;
                    textLabel.text = "Task completed.\nWait for a new task.";
                    Money += MoneyLoseGain;
                    timer = BetweenTasksWaitTime;
                }
            }
            Destroy(slotTransform.GetChild(0).gameObject);
        }
    }
}
