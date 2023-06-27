using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PixelHeart : MonoBehaviour
{

    [SerializeField]
    [Tooltip("the sprite used for a undamaged heart")]
    private Sprite _healthSprite;

    [SerializeField]
    [Tooltip("the sprite used for a damaged heart")]
    private Sprite _damagedSprite;

    private bool _isDamaged = false;
    public bool IsDamaged { get => _isDamaged; }

    private void Start()
    {
        GetComponent<Image>().sprite = _healthSprite;
    }

    public void DamageEvent()
    {
        GetComponent<Image>().sprite = _damagedSprite;
        _isDamaged = true;
    }

}
