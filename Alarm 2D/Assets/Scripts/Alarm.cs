using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Alarm : MonoBehaviour
{
    private const string _alarmAnimationName = "IsAlarming";

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float _volumeStep;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _minVolume;

    private Animator _animator;
    private float _currentVolume;
    private bool _isAlarming;

    private void Awake()
    {
        _isAlarming = false;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(PlayAlarmSound());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(StopAlarmSound());
    }

    private IEnumerator PlayAlarmSound()
    {
        _audioSource.Play();
        _isAlarming = true;
        _animator.SetBool(_alarmAnimationName, _isAlarming);

        while (_currentVolume != _maxVolume)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, _maxVolume, _volumeStep * Time.deltaTime);
            _audioSource.volume = _currentVolume;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator StopAlarmSound()
    {
        while (_currentVolume != _minVolume)
        {
            _currentVolume = Mathf.MoveTowards(_currentVolume, _minVolume, _volumeStep * Time.deltaTime);
            _audioSource.volume = _currentVolume;
            yield return new WaitForEndOfFrame();
        }

        if (_currentVolume == _minVolume)
        {
            StopCoroutine(PlayAlarmSound());
            _audioSource.Stop();
            _isAlarming = false;
            _animator.SetBool(_alarmAnimationName, _isAlarming);
        }        
    }
}