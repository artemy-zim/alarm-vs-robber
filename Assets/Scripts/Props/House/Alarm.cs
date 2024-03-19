using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _volumeChangeFrequency;

    private AudioSource _audioSource;

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

        _audioSource.Play();
        StartCoroutine(OnVolumeChangeCoroutine(_maxVolume));
    }

    public void Stop()
    {
        StartCoroutine(OnFadeOutStopCoroutine());
    }

    private IEnumerator OnFadeOutStopCoroutine()
    {
        yield return StartCoroutine(OnVolumeChangeCoroutine(_minVolume));
        _audioSource.Stop();
    }

    private IEnumerator OnVolumeChangeCoroutine(float targetVolume)
    {
        WaitForEndOfFrame wait = new();

        while(_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _volumeChangeFrequency);

            yield return wait;
        }
    }
}
