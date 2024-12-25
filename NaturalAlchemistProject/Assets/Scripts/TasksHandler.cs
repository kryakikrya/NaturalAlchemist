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
    [SerializeField] private GameObject WinFrame;
    [SerializeField] private GameObject LooseFrame;
    [SerializeField] private AudioSource WinSound;
    [SerializeField] private AudioSource LoseSound;
    [Header("Values")]
    [SerializeField] internal float BetweenTasksWaitTime = 5f;
    internal int Money = 10;
    private Queue<Task> _tasks = new();
    private Task currentTask;
    float timer = 10f;


    private void Start()
    {
        foreach (Task t in possibleTasks)
        {
            _tasks.Enqueue(t);
        }
        if (tutorialtask.MaxTime >0)
        {
            currentTask = tutorialtask;
            textLabel.text = tutorialtask.followuptext;
            timer = tutorialtask.MaxTime;
        }
    }
    private void WinGame()
    {
        Time.timeScale = 0;
        WinFrame.SetActive(true);
    }
    private void LooseGame()
    {
        Time.timeScale = 0;
        LooseFrame.SetActive(true);

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
                LoseSound.Play();
                textLabel.text = "Task failed,you will get fined.\n Wait for a new task.";
                currentTask = null;
                Money -= 5;
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
            else if (currentTask == null && _tasks.Count <= 0 && Money > 0)
            {
                WinGame();
            } 
            if (Money <= 0)
            {
                LooseGame();
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
                    WinSound.Play();
                    textLabel.text = "Task completed.\nWait for a new task.";
                    Money += 2;
                    timer = BetweenTasksWaitTime;
                }
            }
            Destroy(slotTransform.GetChild(0).gameObject);
        }
    }
}
