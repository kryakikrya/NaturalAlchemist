using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MailBoxScript : MonoBehaviour
{
    public string[] messages;
    public TMP_Text messageText;
    public GameObject objectToActivate;
    public float messageChangeTimer = 5f;
    private float timer = 0f;
    void OnMouseDown()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            ShowRandomMessage();
        }
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ShowRandomMessage();
            timer = messageChangeTimer;
        }
    }
    void ShowRandomMessage()
    {
        if (messages.Length > 0)
        {
            int randomIndex = Random.Range(0, messages.Length);

            messageText.text = messages[randomIndex];
        }
        else
        {
            messageText.text = "Нет новых сообщений.";
        }
    }
}


