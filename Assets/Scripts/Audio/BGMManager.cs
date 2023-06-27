using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class BGMManager : MonoBehaviour
{

    [SerializeField]
    [Tooltip("The playlist that will play during the game")]
    private List<AudioClip> _playList= new();
    private int _currentClipIndex = 0;

    private float _timer = 0f;

    private AudioSource _audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Switch<AudioClip>(_playList.Count - 1, Random.Range(0, _playList.Count), _playList);
        Randomize<AudioClip>(_playList);
    }

    private void Update()
    {
        _timer -= Time.unscaledDeltaTime;
        if (_timer < 0)
        {
            AudioClip newClip = _playList[_currentClipIndex];
            _timer = newClip.length + 3;

            _audioSource.PlayOneShot(newClip);
            Debug.Log("now playing : " + newClip.name);

            if (_currentClipIndex == _playList.Count - 1)
            {
                _currentClipIndex = 0;
                Randomize<AudioClip>(_playList);
            }
            else _currentClipIndex++;
        }
    }

    private void Randomize<T>(List<T> workingList)
    {
        if (workingList.Count < 3) return ;

        int maxIndex = workingList.Count - 1;
        for (int i=0; i < maxIndex; i++)
        {
            Switch<T>(i, Random.Range(0, maxIndex),workingList);
        }
        Switch<T>(maxIndex, Random.Range(1, maxIndex),workingList);
    }

    private void Switch<T>(int fstIndex, int sndIndex, List<T> workingList)
    {
        T tmp = workingList[fstIndex];
        workingList[fstIndex] = workingList[sndIndex];
        workingList[sndIndex] = tmp;
    }

}