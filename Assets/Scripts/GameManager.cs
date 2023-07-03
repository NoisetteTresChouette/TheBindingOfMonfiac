using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager m_instance;

    [System.Serializable]
    public class Level
    {
        [Tooltip("The name of the level scene")]
        public string m_sceneName;
        [Tooltip("State if the level is unlocked")]
        public bool m_isUnlocked = false;
    }

    [SerializeField]
    [Tooltip("All the Levels, in order")]
    private List<Level> _levels;
    public List<string> Levels 
    {
        get
        {
            List<string> result = new();
            foreach (Level level in _levels)
            {
                result.Add(level.m_sceneName);
            }
            return result;
        }
    }

    [SerializeField]
    [Tooltip("Set the player prefs LastLevel to the given value")]
    private bool _setLastLevel;
    [SerializeField]
    [Tooltip("If Set Last Level is true, set player prefs LastLevel to this value")]
    private int _lastLevel = 0;

    private int _currentLevel;

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
        if (_setLastLevel)
        {
            PlayerPrefs.SetInt("lastLevel", _lastLevel);
        }
        else _lastLevel = PlayerPrefs.GetInt("lastLevel");

        for (int i = 0; i < _lastLevel + 1; i++)
        {
            _levels[i].m_isUnlocked = true;
        }
    }
    #endregion

    #region LevelsInteraction
    public bool LevelIsUnlocked(int levelNumber)
    {
        return _levels[levelNumber].m_isUnlocked;
    }

    public void SelectLevel(int levelNumber,string levelSceneName)
    {
        _currentLevel = levelNumber;
        SceneManager.LoadScene(levelSceneName);
    }

    public void SelectLevel(int levelNumber, Object levelScene)
    {
        SelectLevel(levelNumber, levelScene.name);
    }

    public void SelectLevel(int levelNumber)
    {
        SelectLevel(levelNumber, "Level" + levelNumber);
    }

    public void LevelWinEvent()
    {
        if (_currentLevel < _levels.Count - 1)
        {
            int nextLevel = _currentLevel + 1;
            if (!_levels[nextLevel].m_isUnlocked)
            {
                _levels[nextLevel].m_isUnlocked = true;
                _lastLevel++;
                PlayerPrefs.SetInt("lastLevel", _lastLevel);
            }
        }
        SceneManager.LoadScene("WinScreen");
    }

    public void LevelLostEvent()
    {
        SceneManager.LoadScene("GameOverScreen");
    }

    public void PlayCurrentLevel()
    {
        SceneManager.LoadScene(_levels[_currentLevel].m_sceneName);
    }

    public void PlayLastLevel()
    {
        _currentLevel = _lastLevel;
        PlayCurrentLevel();
    }

    public void GoNextLevel()
    {
        SceneManager.LoadScene(_levels[++_currentLevel].m_sceneName);
    }

    public bool IsEndOfGame()
    {
        return (_currentLevel == _levels.Count - 1);
    }
    #endregion

    public void PlaySound(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }

}
