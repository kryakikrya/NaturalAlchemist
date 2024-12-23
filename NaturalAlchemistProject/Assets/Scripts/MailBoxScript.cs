using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MailBoxScript : MonoBehaviour
{
    public GameObject objectToActivate;
    void OnMouseDown()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }  // moved to other script
}


