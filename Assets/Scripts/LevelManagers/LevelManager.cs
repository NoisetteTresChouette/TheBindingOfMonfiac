using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class LevelManager : MonoBehaviour
{
    public static LevelManager m_instance;

    [HideInInspector]
    public int m_enemyNumber = 0;

    [HideInInspector]
    public bool m_isPlayerAlive = true;

    #region Events

    public UnityEvent OnLevelWon = new();
    public UnityEvent OnLevelLost = new();

    #endregion

    #region UnityLifeCycle
    private void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
        }
        else m_instance = this;
    }

    private void Start()
    {
        GetComponent<WaveSystem>().StartWave(0);
    }

    private void Update()
    {
        if (m_enemyNumber == 0)
        {
            if (!GetComponent<WaveSystem>().GoToNextWave())
            {
                OnLevelWon.Invoke();
            }
        }
        if (m_isPlayerAlive == false)
        {
            OnLevelLost.Invoke();
        }
    }
    #endregion

    public void LevelWonEvent()
    {
        GameManager.m_instance.LevelWinEvent();
    }

    public void LevelLostEvent()
    {
        GameManager.m_instance.LevelLostEvent();
    }
}
