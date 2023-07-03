using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextLevelButton : MonoBehaviour
{

    [SerializeField]
    [Tooltip("The Game Object to use as first selected if this Game Object is destroyed")]
    private GameObject _nextGO;

    private void Start()
    {
        if (GameManager.m_instance.IsEndOfGame())
        {
            FindObjectOfType<EventSystem>().SetSelectedGameObject(_nextGO);

            Destroy(gameObject);
        }
    }

}
