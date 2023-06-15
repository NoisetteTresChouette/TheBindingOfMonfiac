using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survive : MonoBehaviour
{

    public float m_fireRate = 2f;
    public float m_radius = 10f;

    private float _lastShoot;

    public GameObject BulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - _lastShoot > m_fireRate)
        {
            Vector2 rdmVector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            rdmVector = rdmVector.normalized * m_radius;
            Vector2 rdmPosition = new Vector2(transform.position.x, transform.position.y) - rdmVector;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(rdmVector.x,rdmVector.y,0));
            GameObject bullet = Instantiate(BulletPrefab, rdmPosition, rotation);
            bullet.GetComponent<Bullet>().Init("Enemy");

            _lastShoot = Time.time;
        }
        
    }
}
