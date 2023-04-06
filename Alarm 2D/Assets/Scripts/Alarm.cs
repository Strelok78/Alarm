using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Alarm : MonoBehaviour
{
    private const string AlarmAnimationName = "IsAlarming";

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _volumeStep;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _minVolume;

    private Animator _animator;
    private float _currentVolume;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    internal IEnumerator Alarming(bool alarmStatus)
    {
        var waiter = new WaitForEndOfFrame();

        if (alarmStatus)
        {
            _audioSource.Play();
            _animator.SetBool(AlarmAnimationName, alarmStatus);

            while (_currentVolume != _maxVolume)
            {
                _currentVolume = Mathf.MoveTowards(_currentVolume, _maxVolume, _volumeStep * Time.deltaTime);
                _audioSource.volume = _currentVolume;
                yield return waiter;
            }
        }
        else
        {
            while (_currentVolume != _minVolume)
            {
                _currentVolume = Mathf.MoveTowards(_currentVolume, _minVolume, _volumeStep * Time.deltaTime);
                _audioSource.volume = _currentVolume;
                yield return waiter;
            }

            if (_currentVolume == _minVolume)
            {
                _audioSource.Stop();
                _animator.SetBool(AlarmAnimationName, alarmStatus);
            }
        }
    }
}