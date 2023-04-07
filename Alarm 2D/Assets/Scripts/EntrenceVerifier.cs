using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Alarm))]
public class EntrenceVerifier : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private Alarm _alarm;
    private Coroutine _alarmingCoroutine;
    private bool _isCollided;

    private void Awake()
    {
        _alarm = GetComponent<Alarm>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == _layerMask)
        {
            _isCollided = true;
            UpdateAlarmCoroutine();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _layerMask)
        {
            _isCollided = false;
            UpdateAlarmCoroutine();
        }
    }

    private void UpdateAlarmCoroutine()
    {
        if (_alarmingCoroutine != null)
        {
            StopCoroutine(_alarmingCoroutine);
        }

        _alarmingCoroutine = StartCoroutine(_alarm.Alarming(_isCollided));
    }
}