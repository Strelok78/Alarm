using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(Animator))]
[RequireComponent(typeof(AudioSource), typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    private const string AlarmAnimationName = "IsAlarming";

    [SerializeField] private float _volumeStep;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _minVolume;

    private Animator _animator;
    private AudioSource _audioSource;
    private Coroutine _alarmingCoroutine;
    private float _currentVolume;

    private static readonly WaitForEndOfFrame Wait = new WaitForEndOfFrame();

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetAlarm(bool status)
    {
        float volume;

        if (status)
        {
            ChangeAnimationStatus(status);
            _audioSource.Play();
            volume = _maxVolume;
            ChangeAlarmCoroutineState(volume, status);
        }
        else
        {
            volume = _minVolume;
            ChangeAlarmCoroutineState(volume, status);
        }
    }

    private void TurnOffAlarm(bool status)
    {
        if (_currentVolume == _minVolume && status == false)
        {
            _audioSource.Stop();
            ChangeAnimationStatus(status);
        }
    }

    private void ChangeAlarmCoroutineState(float volume, bool state)
    {
        if (_alarmingCoroutine != null)
        {
            StopCoroutine(_alarmingCoroutine);
        }

        _alarmingCoroutine = StartCoroutine(ChangeVolume(volume, state));
    }

    private void ChangeAnimationStatus(bool isAnimated)
    {
        _animator.SetBool(AlarmAnimationName, isAnimated);
    }

    private IEnumerator ChangeVolume(float targetVolume, bool isAlarming)
    {
        while (_currentVolume != targetVolume)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, targetVolume, _volumeStep * Time.deltaTime);
            _audioSource.volume = _currentVolume;
            yield return Wait;
        }

        TurnOffAlarm(isAlarming);
    }
}