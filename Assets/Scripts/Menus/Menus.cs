using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public void QuitAction()
    {
        Debug.Log("Quitting application");
        Application.Quit();
    }

    public void ResumeAction()
    {
        LevelManager.m_instance.GetComponent<PauseManagement>().UnpauseGame();
    }

    public void PlayCurrentLevel()
    {
        GameManager.m_instance.PlayCurrentLevel();
    }

    public void PlayLastLevel()
    {
        GameManager.m_instance.PlayLastLevel();
    }

    public void GoNextLevel()
    {
        GameManager.m_instance.GoNextLevel();
    }


    public void GoToScene(Object targetScene)
    {
        SceneManager.LoadScene(targetScene.name);
    }

    public void GoToScene(string targetSceneName)
    {
        SceneManager.LoadScene(targetSceneName);
    }

}
