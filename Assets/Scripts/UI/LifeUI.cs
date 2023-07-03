using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{

    [SerializeField]
    [Tooltip("the player it attaches to")]
    private Transform _playerTransform;

    [SerializeField]
    [Tooltip("the prefab for the heart")]
    private GameObject _heartPrefab;
    private List<GameObject> _hearts = new();

    [SerializeField]
    [Tooltip("offset from the player")]
    private Vector3 _offset = new Vector3(0f,-2f,0f);

    private void Start()
    {
        int health = _playerTransform.GetComponent<PlayerLife>().Health;

        for (int i = 0; i< health; i++)
        {
            GameObject newHeart = Instantiate<GameObject>(_heartPrefab, transform);
            _hearts.Add(newHeart);
        }
    }

    private void LateUpdate()
    {
        transform.position = _playerTransform.position + _offset;
    }

    public void DamageEvent()
    {
        for (int i = 0; i<_hearts.Count; i++)
        {
            if (!_hearts[i].GetComponent<PixelHeart>().IsDamaged)
            {
                Debug.Log("oui");
                _hearts[i].GetComponent<PixelHeart>().DamageEvent();
                break;
            }
        }
    }

}
