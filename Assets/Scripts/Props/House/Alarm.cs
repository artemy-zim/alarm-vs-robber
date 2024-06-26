using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _volumeChangeFrequency;

    private AudioSource _audioSource;
    private Coroutine _onVolumeChangeCoroutine;

    private readonly float _minVolume = 0f;
    private readonly float _maxVolume = 1f;

    private void OnValidate()
    {
        _volumeChangeFrequency = Mathf.Clamp(_volumeChangeFrequency, 0.01f, 0.1f);
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (_audioSource.clip == null)
        {
            enabled = false;
            throw new ArgumentNullException(nameof(_audioSource.clip));
        }
    }

    public void Play()
    {
        _audioSource.volume = _minVolume;

        StopVolumeChangeCoroutine();

        _audioSource.Play();
        _onVolumeChangeCoroutine = StartCoroutine(OnVolumeChangeCoroutine(_maxVolume));
    }

    public void Stop()
    {
        StopVolumeChangeCoroutine();

        _onVolumeChangeCoroutine = StartCoroutine(OnFadeOutStopCoroutine());
    }

    private IEnumerator OnFadeOutStopCoroutine()
    {
        yield return StartCoroutine(OnVolumeChangeCoroutine(_minVolume));
        _audioSource.Stop();
    }

    private void StopVolumeChangeCoroutine()
    {
        if (_onVolumeChangeCoroutine != null)
        {
            StopCoroutine(_onVolumeChangeCoroutine);
            _onVolumeChangeCoroutine = null;
        }
    }

    private IEnumerator OnVolumeChangeCoroutine(float targetVolume)
    {
        WaitForEndOfFrame wait = new();

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _volumeChangeFrequency);

            yield return wait;
        }
    }
}
