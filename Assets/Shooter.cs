using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject BulletPrefab;

    private float _timeLastShoot;

    [SerializeField]
    [Tooltip("The time to wait before the player can shoot again")]
    private float _fireRate = 0.25f;

    protected void Shoot(string tag)
    {
        if (Time.time - _timeLastShoot > _fireRate)
        {
            GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Bullet>().Init(tag);
            _timeLastShoot = Time.time;
        }
    }

}
