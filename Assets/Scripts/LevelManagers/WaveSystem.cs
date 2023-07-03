using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{

    [System.Serializable]
    public class Spawn
    {
        public Vector3 m_coordonates;
        public GameObject m_enemyType;
    }

    [System.Serializable]
    public class Wave
    {
        public Spawn[] m_spawns;
    }

    [SerializeField]
    [Tooltip("the waves of enemy to spawn during this level, with each spawn indicating the coordonate end the type of enemy to spawn")]
    private List<Wave> _waves;
    private int _currentWave = 0;

    [SerializeField]
    [Tooltip("The sound that will be played one a new wave spawn")]
    private AudioClip _newWaveSound;
    [SerializeField]
    [Tooltip("The particle systems used during enemy spawning")]
    private ParticleSystem _spawnParticles;

    private IEnumerator SpawnEnemy(Spawn spawn)
    {
        Quaternion rotation = new Quaternion();

        Vector3 coordonates = spawn.m_coordonates;
        Instantiate(_spawnParticles, coordonates, rotation);

        float timer = _spawnParticles.main.duration * (2f / 3f);
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        rotation.eulerAngles = new Vector3(0, 0, -90);
        Instantiate(spawn.m_enemyType, coordonates, rotation);
    }

    public void StartWave(int waveIndex)
    {
        foreach (Spawn spawn in _waves[waveIndex].m_spawns)
        {
            LevelManager.m_instance.m_enemyNumber++;
            StartCoroutine(SpawnEnemy(spawn));
        }
    }

    /*return false if there is no more wave, true otherwise*/
    public bool GoToNextWave()
    {
        if (_currentWave == _waves.Count - 1)
        {
            return false;
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(_newWaveSound);
            _currentWave++;
            StartWave(_currentWave);
            return true;
        }
    }

}
