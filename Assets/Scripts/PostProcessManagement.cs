using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessManagement : MonoBehaviour
{
    private PostProcessVolume _processorVolume;

    [SerializeField]
    [Tooltip("The saturation used for bullet time")]
    private float _bulletTimeSaturation;
    private float _saturationDelta = 0f;

    #region DamageVignette
    [Header("Damage Vignette")]

    [SerializeField]
    [Tooltip("the maximum Intensity of player getting damage vignette")]
    private float _maximumDamageIntensity = 0.54f;

    [SerializeField]
    [Tooltip("the maximum Intensity of player getting damage vignette")]
    private float _damageVignetteSpawnTime = 0.1f;

    [SerializeField]
    [Tooltip("the maximum Intensity of player getting damage vignette")]
    private float _damageVignetteDespawnTime = 0.54f;

    [SerializeField]
    [Tooltip("the maximum Intensity of player getting damage vignette")]
    private float _damageVignetteSustainTime = 0.54f;

    private float _damageTimer;
    #endregion

    private void Awake()
    {
        _processorVolume = GetComponent<PostProcessVolume>();
    }

    public void BulletTimeStartEvent()
    {
        _saturationDelta = _processorVolume.profile.GetSetting<ColorGrading>().saturation.value - _bulletTimeSaturation;
        _processorVolume.profile.GetSetting<ColorGrading>().saturation.value = _bulletTimeSaturation;
    }

    public void BulletTimeStopEvent()
    {
        _processorVolume.profile.GetSetting<ColorGrading>().saturation.value += _saturationDelta;
    }

    public void TriggerPlayerDamagedEvent()
    {
        StartCoroutine(PlayerDamagedEvent());
    }

    private IEnumerator PlayerDamagedEvent()
    {
        _damageTimer = 0f;
        while (_damageTimer < _damageVignetteSpawnTime)
        {
            _damageTimer += Time.deltaTime;
            _processorVolume.profile.GetSetting<Vignette>().intensity.value = Mathf.Lerp(0f, _maximumDamageIntensity, _damageTimer / _damageVignetteSpawnTime);
            yield return new WaitForFixedUpdate();
        }
        
        yield return new WaitForSeconds(_damageVignetteSustainTime);

        _damageTimer = 0f;
        while (_damageTimer < _damageVignetteDespawnTime)
        {
            _damageTimer += Time.deltaTime;
            _processorVolume.profile.GetSetting<Vignette>().intensity.value = Mathf.Lerp(_maximumDamageIntensity,0f, _damageTimer / _damageVignetteDespawnTime);
            yield return new WaitForFixedUpdate();
        }
    }

}
