using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public static GameManager m_instance;

    private float _currentTimeScale;

    [HideInInspector]
    public int m_enemyNumber;

    [HideInInspector]
    public bool m_isPlayerAlive;

    [System.Serializable]
    public class Spawn
    {
        public Vector3 m_coordonates;
        public GameObject m_enemyType;
    }

    [System.Serializable]
    public class Wave
    {
        public Spawn[] m_spawns;
    }

    [SerializeField]
    [Tooltip("the waves of enemy to spawn during this level, with each spawn indicating the coordonate end the type of enemy to spawn")]
    private List<Wave> _waves;

    public AudioClip m_newWaveSound;

    public AudioClip m_pauseSound;

    private int _currentWave = 0;

    private void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
        }
        else m_instance = this;

        m_enemyNumber = 0;
        m_isPlayerAlive = true;
    }

    private void Start()
    {
        StartWave(0);
    }

    private void Update()
    {
        if (m_enemyNumber == 0)
        {
            GoToNextWave();
        }
        if (m_isPlayerAlive == false)
        {
            SceneManager.LoadScene("GameOverScreen");
        }
    }

    private void StartWave(int waveIndex)
    {
        GetComponent<AudioSource>().PlayOneShot(m_newWaveSound);
        foreach (Spawn spawn in _waves[waveIndex].m_spawns)
        {
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, -90);
            Instantiate(spawn.m_enemyType, spawn.m_coordonates,rotation);
            m_enemyNumber++;
        }
    }

    private void GoToNextWave()
    {
        if (_currentWave == _waves.Count - 1)
        {
            SceneManager.LoadScene("WinScreen");
        }
        else
        {
            _currentWave ++;
            StartWave(_currentWave);
        }
    }

    public void OnPause(InputAction.CallbackContext context)
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
        GetComponent<AudioSource>().PlayOneShot(m_pauseSound);
    }

    public void UnpauseGame()
    {
        Time.timeScale = _currentTimeScale;
        SceneManager.UnloadSceneAsync("PauseMenu");
        GetComponent<AudioSource>().PlayOneShot(m_pauseSound);
    }

    public void PlaySound(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }

}
