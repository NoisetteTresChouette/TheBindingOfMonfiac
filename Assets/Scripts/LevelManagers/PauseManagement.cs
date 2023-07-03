using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManagement : MonoBehaviour
{

    private float _currentTimeScale;

    [SerializeField]
    [Tooltip("The sound that will be played when the pause menu appears or disappears")]
    private AudioClip _pauseSound;

    public void PauseAction(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame())
        {
            if (Time.timeScale != 0f)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }
        }
    }

    public void PauseGame()
    {
        _currentTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        GetComponent<AudioSource>().PlayOneShot(_pauseSound);
    }

    public void UnpauseGame()
    {
        Time.timeScale = _currentTimeScale;
        SceneManager.UnloadSceneAsync("PauseMenu");
        GetComponent<AudioSource>().PlayOneShot(_pauseSound);
    }

}
