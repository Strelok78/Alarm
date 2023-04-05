using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmScript : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] float _volumeStep;
    [SerializeField] float _maxVolume;
    [SerializeField] float _minVolume;

    private Animator _animator;
    private float _currentVolume;
    private bool _isAlarming;

    private void Awake()
    {
        _isAlarming = false;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAlarming)
        {
            if (_currentVolume != _maxVolume)
            {
                _currentVolume = Mathf.MoveTowards(_currentVolume, _maxVolume, _volumeStep * Time.deltaTime);
                _audioSource.volume = _currentVolume;
            }
        }
        else
        {
            if (_currentVolume != _minVolume)
            {
                _currentVolume = Mathf.MoveTowards(_currentVolume, _minVolume, _volumeStep * Time.deltaTime);
                _audioSource.volume = _currentVolume;
            }

            if (_currentVolume == _minVolume)
            {
                _audioSource.Stop();
                _animator.SetBool("IsAlarming", _isAlarming);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered: " + collision.name);
        _audioSource.Play();
        _isAlarming = true;
        _animator.SetBool("IsAlarming", _isAlarming);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Left: " + collision.name);
        _isAlarming = false;
    }
}