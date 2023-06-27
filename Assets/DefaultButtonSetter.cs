using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultButtonSetter : MonoBehaviour
{

    [SerializeField]
    [Tooltip("the first game object that will be selected")]
    private GameObject _firstSelected;

    private void Start()
    {
        SetSelected(_firstSelected);   
    }

    public void SetSelected(GameObject newSelected)
    {
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(newSelected);
    }

}
