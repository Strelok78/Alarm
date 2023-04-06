using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Alarm))]
public class CollisionController : MonoBehaviour
{
    private Alarm _alarm;
    private Coroutine _alarmingCoroutine;
    private bool _isCollided;

    private void Awake()
    {
        _alarm = GetComponent<Alarm>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _isCollided = true;
        UpdateAlarmCoroutine();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isCollided = false;
        UpdateAlarmCoroutine();
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