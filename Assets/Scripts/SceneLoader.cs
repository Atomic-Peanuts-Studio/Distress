using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator _animator;

    public void LoadScene(string sceneName)
    {
        MenuScore.LastScore = 0;
        SceneManager.LoadScene("Playable_Demo_LEVEL_GEN");
    }

    public void StartAnimation()
    {
        _animator.SetTrigger("ClickedStart");
    }

    
}
