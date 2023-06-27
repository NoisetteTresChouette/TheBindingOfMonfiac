using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitAction()
    {
        Debug.Log("Quitting application");
        Application.Quit();
    }

    public void ResumeAction()
    {
        GameManager.m_instance.UnpauseGame();
    }

}
