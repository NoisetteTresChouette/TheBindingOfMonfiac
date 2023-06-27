using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The audio mixer the slider will set up to")]
    private AudioMixer _mixer;

    [SerializeField]
    [Tooltip("The parameter of the audio mixer to set up to")]
    private string _parameter;
    
    private void Start()
    {
        _mixer.GetFloat(_parameter,out float value);
        GetComponent<Slider>().value = Mathf.Pow(10f,value/20f);
    }

    public void SetVolume(float value)
    {

        _mixer.SetFloat(_parameter, 20f*Mathf.Log10(value));

    }

}
