using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Speed of the bullet")]
    private float _speed = 10f;

    private float _lifeSpend = 1.2f;

    private Rigidbody2D _rigidbody;

    public ParticleSystem m_explosionEffect;

    public AudioClip m_explosionSound;
    public AudioMixerGroup m_mixerGroup;

    #region Unity Life Cycle
    private void Awake()
    { 
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0f;
    }

    void Start()
    {
        _rigidbody.velocity = transform.up * _speed;
        Destroy(gameObject, _lifeSpend);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != tag)
        {
            Instantiate(m_explosionEffect, transform.position, transform.rotation);
            GameObject audioSource = new();
            audioSource.AddComponent<AudioSource>();
            audioSource.GetComponent<AudioSource>().outputAudioMixerGroup = m_mixerGroup;
            audioSource.GetComponent<AudioSource>().PlayOneShot(m_explosionSound);
            Destroy(audioSource, m_explosionSound.length);
            Destroy(gameObject);
        }
    }

    #endregion

    public void Init(string teamTag, int layer, float range)
    {
        tag = teamTag;
        gameObject.layer = layer;
        _lifeSpend = range / _speed;
    }

}
