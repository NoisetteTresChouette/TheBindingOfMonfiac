using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{

    [SerializeField]
    [Tooltip("The number of the level")]
    private int _levelNumber;

    [SerializeField]
    [Tooltip("Drag and drop the level scene there")]
    private Object _levelScene;
    [SerializeField]
    [Tooltip("The name of the level scene")]
    private string _levelSceneName;

    [SerializeField]
    [Tooltip("The name of the level that will appear on the sign")]
    private string _levelName;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        
    }

    private void Start()
    {
        SetName();
        SetButton();
    }

    public void SetName()
    {
        if (_levelName == "")
        {
            if (_levelScene != null)
            {
                _levelName = _levelScene.name;
            }
            else if (_levelSceneName != "")
            {
                _levelName = _levelSceneName;
            }
            else
            {
                _levelName = $"Level{_levelNumber}";
            }
        }
        GetComponentInChildren<TMPro.TextMeshProUGUI>().SetText(_levelName);
    }

    public void Init(int levelNumber, string levelSceneName,string levelName)
    {
        _levelNumber = levelNumber;
        _levelSceneName = levelSceneName;
        _levelName = levelName;
    }

    public void Init(int levelNumber, string levelSceneName)
    {
        _levelNumber = levelNumber;
        _levelSceneName = levelSceneName;
    }

    public void SetButton()
    {
        if (GameManager.m_instance.LevelIsUnlocked(_levelNumber))
        {
            _button.interactable = true;
            GetComponentInChildren<TMPro.TextMeshProUGUI>().SetText(_levelName);
        }
        else
        {
            _button.interactable = false;
            GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "-?-?-?-";
        }
    }

    public void SelectLevel()
    {
        if (_levelScene != null)
            GameManager.m_instance.SelectLevel(_levelNumber, _levelScene);
        else if (_levelSceneName != null)
            GameManager.m_instance.SelectLevel(_levelNumber, _levelSceneName);
        else 
            GameManager.m_instance.SelectLevel(_levelNumber);
    }

}
