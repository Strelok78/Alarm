using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Alarm : MonoBehaviour
{
    private const string AlarmAnimationName = "IsAlarming";

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _volumeStep;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _minVolume;

    private Animator _animator;
    private Coroutine _alarmingCoroutine;
    private float _currentVolume;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetAlarm(bool status)
    {
        float volume;

        if (status)
            volume = _maxVolume;
        else
            volume = _minVolume;

        if (_alarmingCoroutine != null)
        {
            StopCoroutine(ChangeVolume(volume));
        }

        ChangeAlarmSatus(status);
        ChangeAnimationStatus(status);
        _alarmingCoroutine = StartCoroutine(ChangeVolume(volume));
    }

    private void ChangeAlarmSatus(bool isTurnedOn)
    {
        if (isTurnedOn)
            _audioSource.Play();
        else
            _audioSource.Stop();
    }

    private void ChangeAnimationStatus(bool isAnimated)
    {
        _animator.SetBool(AlarmAnimationName, isAnimated);
    }

    internal IEnumerator ChangeVolume(float targetVolume)
    {
        var waiter = new WaitForEndOfFrame();

        while (_currentVolume != targetVolume)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, targetVolume, _volumeStep * Time.deltaTime);
            _audioSource.volume = _currentVolume;
            yield return waiter;
        }
    }
}