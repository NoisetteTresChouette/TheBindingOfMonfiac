using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public void Die()
    {
        GameManager.m_instance.m_enemyNumber--;
    }

}
