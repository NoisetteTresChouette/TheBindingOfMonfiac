using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Shooter : MonoBehaviour
{
    public GameObject BulletPrefab;

    public AudioClip _shootingSound;
    public AudioSource _audioSource;

    private float _timeLastShoot;

    [SerializeField]
    [Tooltip("The time to wait before the player can shoot again")]
    private float _fireRate = 0.25f;

    [SerializeField]
    [Tooltip("The range the bullets can reach")]
    protected float _range = 12f;

    private void Awake()
    {
        _timeLastShoot = _fireRate - 0.1f;
    }

    protected bool Shoot(string tag)
    {
        if (Time.time - _timeLastShoot > _fireRate && Time.timeScale != 0f)
        {
            GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
            int layer;
            switch (tag)
            {
                case "Player":
                case "Ally":
                    layer = 9;/*AllyProjectiles*/
                    break;
                case "Enemy":
                    layer = 7;/*EnemyProjectiles*/
                    break;
                default:
                    layer = 0;
                    break;
            }
            bullet.GetComponent<Bullet>().Init(tag,layer,_range);
            _timeLastShoot = Time.time;

            _audioSource.PlayOneShot(_shootingSound);

            return true;
        }
        else return false;
    }

    public float GetRange()
    {
        return _range;
    }

}
