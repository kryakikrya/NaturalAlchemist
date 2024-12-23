using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookScript : MonoBehaviour
{
    public GameObject objectToActivate;

    // Метод, вызываемый при клике на объект
    void OnMouseDown()
    {
        if (objectToActivate != null)
        {
            // Активируем указанный объект
            objectToActivate.SetActive(true);
        }
    }
}
