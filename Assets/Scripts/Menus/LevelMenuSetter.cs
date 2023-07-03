using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelMenuSetter : MonoBehaviour
{

    public GameObject m_LevelButtonPrefab;

    [System.Serializable]
    public class LevelName
    {
        public string m_levelName;
        public int m_levelNumber;
    }
    [SerializeField]
    [Tooltip("Give custom names to the levels")]
    private List<LevelName> _levelNames = new();

    private void Start()
    {

        List<string> levelSceneNames = GameManager.m_instance.Levels;
        int len = levelSceneNames.Count;

        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100f * len);

        string[] givenNames = new string[len];
        
        foreach (LevelName levelName in _levelNames)
        {
            int number = levelName.m_levelNumber;
            if (number < len)
                givenNames[number] = levelName.m_levelName;
        }

        int firstIndex = FindObjectsOfType<LevelButton>().Length;

        for (int i = firstIndex; i<levelSceneNames.Count; i++)
        {
            GameObject levelButton = Instantiate(m_LevelButtonPrefab,transform);

            if (givenNames[i] != null)
            {
                levelButton.GetComponent<LevelButton>().Init(i, levelSceneNames[i],givenNames[i]);
            }
            else
            {
                levelButton.GetComponent<LevelButton>().Init(i, levelSceneNames[i]);
            }
        }
    }

}
