using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public void OnTriggerEnter2D()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
