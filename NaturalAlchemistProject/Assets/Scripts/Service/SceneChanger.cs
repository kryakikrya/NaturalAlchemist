using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] int SceneID;

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneID);
    }
}
